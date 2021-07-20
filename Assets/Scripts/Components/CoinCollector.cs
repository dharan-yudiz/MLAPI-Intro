using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

namespace MLAPI.Demo {

    public class CoinCollector : NetworkBehaviour
    {
        NetworkObject c_Collider;

        private void OnTriggerEnter(Collider other) {
            if (other.tag != "coin") {
                c_Collider = other.gameObject.GetComponent<NetworkObject>();
                OnDestroyCoinServerRpc();
            }

        }


        [ServerRpc]
        void OnDestroyCoinServerRpc() {
            if (c_Collider != null)
                c_Collider.Despawn();
        }

    }

}
