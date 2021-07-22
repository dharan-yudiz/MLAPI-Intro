
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable.Collections;
using System;
using MLAPI.NetworkVariable;
using System.Collections.Generic;
using UnityEngine;
using MLAPI.Transports;
using System.IO;

namespace MLAPI.Demo
{
    public class Chat : NetworkBehaviour
    {
        private NetworkList<string> _chatMessages = new NetworkList<string>(new NetworkVariable.NetworkVariableSettings
        {
            ReadPermission = NetworkVariablePermission.Everyone,
            WritePermission = NetworkVariablePermission.Everyone,
            SendTickrate = 5

        }, new List<string>());

        public NetworkList<string> ChatMessages => _chatMessages;

        public void onSendMessage(string Text)
        {
            ChatMessages.Add("<b>" + GameManager.Instance.currentPlayerData.PlayerName + "</b> : " + Text);
        }
    }

}