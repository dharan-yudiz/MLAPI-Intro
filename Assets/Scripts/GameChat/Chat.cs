
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
    [Serializable]
    public class Message
    {
        public string Playername;
        public string MessageText;

        public Message(string name, string msgtext)
        {
            Playername = name;
            MessageText = msgtext;
        }

    }

    public class Chat : NetworkBehaviour
    {
        private NetworkList<string> _chatMessages = new NetworkList<string>(new NetworkVariable.NetworkVariableSettings
        {
            ReadPermission = NetworkVariable.NetworkVariablePermission.Everyone,
            WritePermission = NetworkVariable.NetworkVariablePermission.Everyone,
            SendTickrate = 5

        }, new List<string>());

        public NetworkList<string> ChatMessages => _chatMessages;

        public void onSendMessage(string Text)
        {
            ChatMessages.Add(Text);
        }
    }

}