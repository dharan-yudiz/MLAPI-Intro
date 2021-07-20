using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace MLAPI.Demo
{
    public class PlayerHandler : NetworkBehaviour
    {
        public TextMeshPro PlayerText;
        [SerializeField] Rigidbody rigidbody;

        private void Start() {
            if (!IsLocalPlayer) {
                Destroy(rigidbody);
            }
        }

    }
}