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
        [SerializeField] public int score;

        NetworkVariable<int> displayScore = new NetworkVariable<int>();

        private NetworkVariableString displayName = new NetworkVariableString();
        
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
            displayScore.OnValueChanged += HandleDisplayScoreChanges;
        }


        private void OnDisable()
        {
            displayName.OnValueChanged -= HandleDisplayNameChanged;
            displayScore.OnValueChanged -= HandleDisplayScoreChanges;
        }

        private void HandleDisplayNameChanged(string oldDisplayName, string newDisplayName)
        {
            PlayerText.text = newDisplayName;
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