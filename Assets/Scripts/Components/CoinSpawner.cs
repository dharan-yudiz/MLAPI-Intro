using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

namespace MLAPI.Demo
{

    public class CoinSpawner : NetworkBehaviour
    {
        [SerializeField] NetworkObject coinPrefab;
        [SerializeField] Vector2 xPosition;
        [SerializeField] Vector2 zPosition;


        private void OnEnable()
        {
            CustomNetworkManager.Instance.OnServerStarted += OnServerStart;
            Events.OnCoinCollected += SpawnCoinServerRpc;
        }

        private void OnDisable()
        {
            CustomNetworkManager.Instance.OnServerStarted -= OnServerStart;
            Events.OnCoinCollected -= SpawnCoinServerRpc;

        }

        void OnServerStart()
        {
            if (IsServer || IsHost)
                SpawnCoinServerRpc();
        }

        public override void NetworkStart()
        {
            base.NetworkStart();
        }

        [ServerRpc]
        void SpawnCoinServerRpc()
        {
            float randomeXposition = Random.Range(xPosition.x, xPosition.y);
            float randomeZposition = Random.Range(zPosition.x, zPosition.y);


            NetworkObject coin = Instantiate(coinPrefab, new Vector3(randomeXposition, 0, randomeZposition), Quaternion.identity, transform);
            coin.Spawn();
        }



    }
}
