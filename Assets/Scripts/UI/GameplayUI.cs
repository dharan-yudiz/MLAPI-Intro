using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MLAPI.Demo
{
    public class GameplayUI : ScreenView
    {
        [SerializeField] Text scoreText;
        [SerializeField] Text nameText;

        public ChatUI chatUI;
        public override void OnScreenShowCalled() {
            base.OnScreenShowCalled();
            nameText.text = GameManager.Instance.currentPlayerData.PlayerName;
            scoreText.text = $"Score: {GameManager.Instance.Score}";
            chatUI.StartListeningForMessages();
        }

        private void OnEnable() {
            Events.OnCoinCollected += OnScoreChanged;
        }

        private void OnDisable() {
            Events.OnCoinCollected -= OnScoreChanged;
        }

        void OnScoreChanged() {
            scoreText.text = $"Score: {GameManager.Instance.Score}";
        }

    }
}