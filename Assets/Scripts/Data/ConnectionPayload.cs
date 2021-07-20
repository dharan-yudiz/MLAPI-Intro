namespace MLAPI.Demo
{
    [System.Serializable]
    public class ConnectionPayload
    {
        public string PlayerName;
        public string Password;

        public ConnectionPayload(string name, string password)
        {
            this.PlayerName = name;
            this.Password = password;
        }
    }

}