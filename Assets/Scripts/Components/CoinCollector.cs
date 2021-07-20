using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

namespace MLAPI.Demo {

    public class CoinCollector : NetworkBehaviour
    {
        bool isCollected;
        private void OnTriggerEnter(Collider other) {

            if (other.tag == "Player" && !isCollected) {
                other.gameObject.GetComponent<PlayerHandler>().OnIncereseScoreServerRpc();
                OnDestroyCoinServerRpc();
                Events.CoinCollected();
                isCollected = true;
            }

        }

        [ServerRpc]
        void OnDestroyCoinServerRpc() {
            Destroy(gameObject);
        }

    }

}
