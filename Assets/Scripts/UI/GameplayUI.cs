using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MLAPI.Demo
{
    public class GameplayUI : ScreenView
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI nameText;

        public override void OnScreenShowCalled() {
            base.OnScreenShowCalled();
            nameText.text = GameManager.Instance.currentPlayerData.PlayerName;
            scoreText.text = $"Score: {GameManager.Instance.Score}";
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