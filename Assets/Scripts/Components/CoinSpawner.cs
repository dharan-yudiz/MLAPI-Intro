using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

namespace MLAPI.Demo {

    public class CoinSpawner : NetworkBehaviour {
        [SerializeField] NetworkObject coinPrefab;
        [SerializeField] Vector2 xPosition;
        [SerializeField] Vector2 zPosition;

        private void OnEnable() {
            CustomNetworkManager.Instance.OnServerStarted += OnHostStart;
        }

        private void OnDisable() {
            CustomNetworkManager.Instance.OnServerStarted -= OnHostStart;
        }

        void OnHostStart() {
            if (IsHost)
                SpawnCoinServerRpc();
        }

        public override void NetworkStart() {
            base.NetworkStart();
            Debug.Log("Start Network");
        }

        [ServerRpc]
        void SpawnCoinServerRpc() {
            StartCoinGenration();
        }

        void StartCoinGenration() {
            StartCoroutine(OnGenerateAfterDelay());
        }

        IEnumerator OnGenerateAfterDelay() {
            int second = Random.Range(1, 10);
            Debug.Log("Start Spawning " + second);
            yield return new WaitForSeconds(second);
            OnGenerateCoin();
            StartCoroutine(OnGenerateAfterDelay());
        }

        void OnGenerateCoin() {
            float randomeXposition = Random.Range(xPosition.x, xPosition.y);
            float randomeZposition = Random.Range(zPosition.x, zPosition.y);

            Vector3 position = new Vector3(randomeXposition, 0, randomeZposition);

            NetworkObject coin = Instantiate(coinPrefab, position, Quaternion.identity, transform);
            coin.Spawn();
        }

    }
}
