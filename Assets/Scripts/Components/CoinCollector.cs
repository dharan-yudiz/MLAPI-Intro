using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Connection;

namespace MLAPI.Demo
{

    public class CoinCollector : NetworkBehaviour
    {
        bool isCollected;
        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Player" && !isCollected)
            {
                OnDestroyCoinServerRpc();
                //UpdateScoreServerRpc(other.gameObject.GetComponent<PlayerHandler>().OwnerClientId);
                PlayerHandler handler = other.gameObject.GetComponent<PlayerHandler>();

                handler.Score.Value++;

                if (handler.IsLocalPlayer)
                    GameManager.Instance.Score++;

                Events.CoinCollected();
                isCollected = true;
            }

        }

        [ServerRpc]
        void OnDestroyCoinServerRpc()
        {
            Destroy(gameObject);
        }   
        
        [ServerRpc]
        void UpdateScoreServerRpc(ulong playerID)
        {
            CustomNetworkManager.GetPlayerData(playerID).PlayerScore++;

            if (!CustomNetworkManager.Singleton.ConnectedClients.TryGetValue(playerID, out NetworkClient networkClient))
                return;

            if (!networkClient.PlayerObject.TryGetComponent<PlayerHandler>(out PlayerHandler playerHandler))
                return;

            playerHandler.Score.Value++;
        }


    }

}
