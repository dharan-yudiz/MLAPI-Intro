using MLAPI;
using MLAPI.Transports;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MLAPI.Demo
{
    public class CustomNetworkManager : NetworkManager
    {
        static CustomNetworkManager _instance;
        private static Dictionary<ulong, PlayerData> ClientData;
        string serverPassword;


        public static CustomNetworkManager Instance => _instance;



        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {

            OnServerStarted += HandleServerStarted;
            OnClientConnectedCallback += HandleClientConnected;
            Singleton.OnClientDisconnectCallback += HandleClientDisconnect;
        }

        private void OnDestroy()
        {
            // Prevent error in the editor
            if (Singleton == null) { return; }

            OnServerStarted -= HandleServerStarted;
            OnClientConnectedCallback -= HandleClientConnected;
            OnClientDisconnectCallback -= HandleClientDisconnect;

            if (IsServer || IsHost)
            {
                StopServer();
            }

        }



        public void Host(string password)
        {
            serverPassword = password;

            ClientData = new Dictionary<ulong, PlayerData>();
            ClientData[LocalClientId] = GameManager.Instance.currentPlayerData;

            ConnectionApprovalCallback += ApprovalCheck;
            StartHost();
        }


        public void Client(ConnectionPayload connectionPayload)
        {
            var payload = JsonUtility.ToJson(connectionPayload);


            NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes(payload);
            Singleton.StartClient();
        }

        public void Leave()
        {
            if (IsHost)
            {
                StopHost();
                ConnectionApprovalCallback -= ApprovalCheck;
            }
            else if (IsClient)
            {
                StopClient();
            }

            UIController.instance.HideCurrentScreen();
            UIController.instance.ShowThisScreen(ScreenType.StartGame, EnableDirection.Forward);
        }



        public static PlayerData? GetPlayerData(ulong clientId)
        {
            if (ClientData.TryGetValue(clientId, out PlayerData playerData))
            {
                return playerData;
            }

            return null;
        }

        #region Event Callbacks

        private void ApprovalCheck(byte[] connectionData, ulong clientID, ConnectionApprovedDelegate callback)
        {
            string payload = System.Text.Encoding.ASCII.GetString(connectionData);
            var connectionPayload = JsonUtility.FromJson<ConnectionPayload>(payload);


            bool approveConnection = connectionPayload.Password == serverPassword;


            Vector3 spawnPos = Vector3.zero;
            Quaternion spawnRot = Quaternion.identity;

            if (approveConnection)
            {

                switch (ConnectedClients.Count)
                {
                    case 1:
                        spawnPos = new Vector3(0f, 0f, 0f);
                        spawnRot = Quaternion.Euler(0f, 180f, 0f);
                        break;
                    case 2:
                        spawnPos = new Vector3(2f, 0f, 0f);
                        spawnRot = Quaternion.Euler(0f, 225, 0f);
                        break;
                }


                ClientData[clientID] = new PlayerData(connectionPayload.PlayerName, connectionPayload.PlayerColor);

            }
            else
            {
                Debug.Log("Connection rejected");
            }

            callback(true, null, approveConnection, spawnPos, spawnRot);

        }
        private void HandleClientDisconnect(ulong obj)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                ClientData.Remove(obj);
            }

            if (obj.Equals(LocalClientId))
            {
                UIController.instance.HideCurrentScreen();
                UIController.instance.ShowThisScreen(ScreenType.StartGame, EnableDirection.Forward);
            }
        }

        private void HandleClientConnected(ulong obj)
        {
            if (obj.Equals(LocalClientId))
            {
                UIController.instance.HideCurrentScreen();
                UIController.instance.ShowThisScreen(ScreenType.GamePlay, EnableDirection.Forward);
            }
        }

        private void HandleServerStarted()
        {
            if (IsHost)
            {
                HandleClientConnected(ServerClientId);
            }
        }

        private void OnConnectedToServer()
        {
            Debug.Log("Connected to server");
        }

        #endregion
    }
}
