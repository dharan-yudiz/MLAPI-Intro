using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

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
                Events.CoinCollected();
                UpdateScoreServerRpc(other.gameObject.GetComponent<PlayerHandler>().OwnerClientId);
                GameManager.Instance.Score++;
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
            //player.Score.Value++;

        }

    }

}
