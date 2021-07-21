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
        [SerializeField] int score;

        public Renderer renderer;



        private NetworkVariable<int> displayScore = new NetworkVariable<int>();
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
            score = newValue;
        }


    }
}