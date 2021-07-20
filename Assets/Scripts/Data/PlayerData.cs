namespace MLAPI.Demo
{
    [System.Serializable]
    public class PlayerData
    {
        public string PlayerName { get; private set; }

        public PlayerData(string playerName)
        {
            PlayerName = playerName;
        }
    }

}