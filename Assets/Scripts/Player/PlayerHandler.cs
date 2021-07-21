using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using System;

namespace MLAPI.Demo
{
    public class PlayerHandler : NetworkBehaviour
    {
        public TextMeshPro PlayerText;

        public Renderer renderer;

        public int PlayerScore;


        private NetworkVariable<int> displayScore = new NetworkVariable<int>(new NetworkVariableSettings {

            ReadPermission = NetworkVariablePermission.Everyone,
            WritePermission = NetworkVariablePermission.Everyone

        });
        private NetworkVariableString displayName = new NetworkVariableString();
        private NetworkVariableColor playerColor = new NetworkVariableColor();

        public NetworkVariable<int> Score
        {
            get => displayScore; set { displayScore = value; }
        }

        public override void NetworkStart()
        {
            if (!IsServer) { return; }

            PlayerData? playerData = CustomNetworkManager.GetPlayerData(OwnerClientId);

            if (playerData != null)
            {
                displayName.Value = playerData.PlayerName;
                playerColor.Value = playerData.PlayerColor;
            }
        }



        private void OnEnable()
        {
            displayName.OnValueChanged += HandleDisplayNameChanged;
            displayScore.OnValueChanged += HandleDisplayScoreChanges;
            playerColor.OnValueChanged += HandlePlayerColorChanged;
        }


        private void OnDisable()
        {
            displayName.OnValueChanged -= HandleDisplayNameChanged;
            displayScore.OnValueChanged -= HandleDisplayScoreChanges;

            playerColor.OnValueChanged -= HandlePlayerColorChanged;
        }

        private void HandleDisplayNameChanged(string oldDisplayName, string newDisplayName)
        {
            PlayerText.text = newDisplayName;
        }
        private void HandlePlayerColorChanged(Color previousValue, Color newValue)
        {
            renderer.material.color = newValue;
        }

        private void HandleDisplayScoreChanges(int previousValue, int newValue)
        {
            PlayerScore = newValue;

        }


    }
}