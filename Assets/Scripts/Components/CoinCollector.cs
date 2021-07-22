using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Connection;
using DG.Tweening;

namespace MLAPI.Demo
{

    public class CoinCollector : NetworkBehaviour
    {
        bool isCollected;

        private void Start()
        {
            transform.DORotate(new Vector3(-45,360f, -45), 1f,RotateMode.WorldAxisAdd).SetLoops(-1,LoopType.Yoyo);
        }

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
                DOTween.KillAll();
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
