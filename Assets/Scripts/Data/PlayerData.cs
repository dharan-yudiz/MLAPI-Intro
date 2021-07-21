using UnityEngine;

namespace MLAPI.Demo
{
    [System.Serializable]
    public class PlayerData
    {
        public string PlayerName;
        public Color PlayerColor;

        public PlayerData(string playerName,Color color)
        {
            PlayerName = playerName;
            PlayerColor = color;
        }
    }

}