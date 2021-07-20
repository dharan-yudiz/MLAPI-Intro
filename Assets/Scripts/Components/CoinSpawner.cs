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
            Events.OnCoinCollected += SpawnCoinServerRpc;
        }

        private void OnDisable() {
            CustomNetworkManager.Instance.OnServerStarted -= OnHostStart;
            Events.OnCoinCollected -= SpawnCoinServerRpc;

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
            if (IsHost)
                OnGenerateCoin();
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
