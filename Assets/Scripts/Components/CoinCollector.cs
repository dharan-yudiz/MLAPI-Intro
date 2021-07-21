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
                PlayerHandler handler = other.gameObject.GetComponent<PlayerHandler>();

                handler.Score.Value++;


                if (IsServer || IsHost)
                {
                    NotifyPlayersClientRpc(CustomNetworkManager.GetPlayerData(handler.OwnerClientId).PlayerName);
                }


                if (handler.IsLocalPlayer)
                    GameManager.Instance.Score++;

                Events.CoinCollected();
                isCollected = true;
            }

        }

        [ServerRpc]
        void OnDestroyCoinServerRpc()
        {
            if (IsOwner)
            {
                Destroy(gameObject);
            }
        }


        [ClientRpc]
        public void NotifyPlayersClientRpc(string Playername)
        {
            if (Playername != GameManager.Instance.currentPlayerData.PlayerName)
            {
                NonIntrusivePopup.Instance.Show(Playername + " has collected a coin!");
            }
        }

    }

}
