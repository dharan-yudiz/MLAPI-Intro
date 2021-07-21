
using MLAPI;
using MLAPI.NetworkVariable.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


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
        private NetworkList<Message> _chatMessages = new NetworkList<Message>(new NetworkVariable.NetworkVariableSettings
        {
            ReadPermission = NetworkVariable.NetworkVariablePermission.Everyone,
            WritePermission = NetworkVariable.NetworkVariablePermission.Everyone,
            SendTickrate = 5

        }, new List<Message>());

        public NetworkList<Message> ChatMessages => _chatMessages;


        public void onSendMessage(string Text)
        {
            ChatMessages.Add(new Message(GameManager.Instance.currentPlayerData.PlayerName,Text));
        }
    }
}