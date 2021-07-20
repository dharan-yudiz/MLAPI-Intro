using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MLAPI.NetworkVariable;

namespace MLAPI.Demo
{
    public class PlayerHandler : NetworkBehaviour
    {
        public TextMeshPro PlayerText;
        [SerializeField] Rigidbody rigidbody;

        private NetworkVariableString displayName = new NetworkVariableString();
        
        
        private void Start() {
            if (!IsLocalPlayer) {
                Destroy(rigidbody);
            }
        }


        public override void NetworkStart()
        {
            if (!IsServer) { return; }

            PlayerData? playerData = CustomNetworkManager.GetPlayerData(OwnerClientId);

            if (playerData != null)
            {
                displayName.Value = playerData.PlayerName;
            }
        }

        private void OnEnable()
        {
            displayName.OnValueChanged += HandleDisplayNameChanged;
        }

        private void OnDisable()
        {
            displayName.OnValueChanged -= HandleDisplayNameChanged;
        }

        private void HandleDisplayNameChanged(string oldDisplayName, string newDisplayName)
        {
            PlayerText.text = newDisplayName;
        }


    }
}