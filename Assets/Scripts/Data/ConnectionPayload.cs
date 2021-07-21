using UnityEngine;

namespace MLAPI.Demo
{
    [System.Serializable]
    public class ConnectionPayload
    {
        public string PlayerName;
        public string Password;
        public Color PlayerColor;

        public ConnectionPayload(string name, string password, Color color)
        {
            this.PlayerName = name;
            this.Password = password;
            this.PlayerColor = color;
        }
    }

}