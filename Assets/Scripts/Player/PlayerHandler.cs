using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MLAPI.NetworkVariable;
<<<<<<< Updated upstream:Assets/PlayerHandler.cs
using MLAPI.Messaging;
=======
>>>>>>> Stashed changes:Assets/Scripts/Player/PlayerHandler.cs
using System;

namespace MLAPI.Demo
{
    public class PlayerHandler : NetworkBehaviour
    {
        public TextMeshPro PlayerText;
<<<<<<< Updated upstream:Assets/PlayerHandler.cs
        [SerializeField] public int score;

        NetworkVariable<int> displayScore = new NetworkVariable<int>();
=======
        public Renderer renderer;

        [SerializeField] Rigidbody rigidbody;
>>>>>>> Stashed changes:Assets/Scripts/Player/PlayerHandler.cs

        private NetworkVariableString displayName = new NetworkVariableString();
        private NetworkVariableColor playerColor = new NetworkVariableColor();

        
        public override void NetworkStart()
        {
            if (!IsServer) { return; }

            PlayerData? playerData = CustomNetworkManager.GetPlayerData(OwnerClientId);

            if (playerData != null)
            {
                displayName.Value = playerData.PlayerName;
                renderer.material.color = playerData.PlayerColor;
            }
        }

        private void OnEnable()
        {
            displayName.OnValueChanged += HandleDisplayNameChanged;
<<<<<<< Updated upstream:Assets/PlayerHandler.cs
            displayScore.OnValueChanged += HandleDisplayScoreChanges;
=======
            playerColor.OnValueChanged += HandlePlayerColorChanged;
>>>>>>> Stashed changes:Assets/Scripts/Player/PlayerHandler.cs
        }


        private void OnDisable()
        {
            displayName.OnValueChanged -= HandleDisplayNameChanged;
<<<<<<< Updated upstream:Assets/PlayerHandler.cs
            displayScore.OnValueChanged -= HandleDisplayScoreChanges;
=======
            playerColor.OnValueChanged -= HandlePlayerColorChanged;
>>>>>>> Stashed changes:Assets/Scripts/Player/PlayerHandler.cs
        }

        private void HandleDisplayNameChanged(string oldDisplayName, string newDisplayName)
        {
            PlayerText.text = newDisplayName;
        }
        private void HandlePlayerColorChanged(Color previousValue, Color newValue)
        {
            renderer.material.color = newValue;
        }


        private void HandleDisplayScoreChanges(int previousValue, int newValue) {
            score = newValue;
        }

        [ServerRpc]
        public void OnIncereseScoreServerRpc() {
            displayScore.Value++;
        }

    }
}