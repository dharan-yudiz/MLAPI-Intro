using UnityEngine;

namespace MLAPI.Demo
{
    [System.Serializable]
    public class PlayerData
    {
        public string PlayerName;
        public Color PlayerColor;
        public int PlayerScore;

        public PlayerData(string playerName,Color color)
        {
            PlayerName = playerName;
            PlayerColor = color;
        }
    }

}