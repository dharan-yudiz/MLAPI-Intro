using MLAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MLAPI.Demo
{
    public class CustomNetworkManager : NetworkManager
    {
        static CustomNetworkManager _instance;
        public static CustomNetworkManager Instance => _instance;


        public string Password;


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
        }


        public void Host()
        {
            ConnectionApprovalCallback += ApprovalCheck;
            StartHost();
        }


        public void Client(string password)
        {
            NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes(password);
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
            UIController.instance.ShowThisScreen(ScreenType.MainMenu, EnableDirection.Forward);
        }



        #region Event Callbacks

        private void ApprovalCheck(byte[] connectionData, ulong arg2, ConnectionApprovedDelegate callback)
        {
            string password = System.Text.Encoding.ASCII.GetString(connectionData);

            bool approveConnection = password == Password;

            Vector3 spawnPos = Vector3.zero;
            Quaternion spawnRot = Quaternion.identity;

            switch (NetworkManager.Singleton.ConnectedClients.Count)
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

            callback(true, null, approveConnection, spawnPos, spawnRot);
        }
        private void HandleClientDisconnect(ulong obj)
        {
            if (obj.Equals(LocalClientId))
            {
                UIController.instance.HideCurrentScreen();
                UIController.instance.ShowThisScreen(ScreenType.MainMenu, EnableDirection.Forward);
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
        #endregion
    }
}
