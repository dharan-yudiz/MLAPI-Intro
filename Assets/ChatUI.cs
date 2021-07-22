using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MLAPI.Demo
{
    public class ChatUI : NetworkBehaviour
    {
        Chat ChatNetworking;

        public InputField MessageField;
        public Text MessagesText;

        private void Awake()
        {
            ChatNetworking = GetComponent<Chat>();
        }

        public void StartListeningForMessages()
        {
            PopulateList();
            ChatNetworking.ChatMessages.OnListChanged += onRecieveMessage;
        }

        private void OnDisable()
        {
            ChatNetworking.ChatMessages.OnListChanged -= onRecieveMessage;
        }

        private void PopulateList()
        {
            string text = "";

            if (ChatNetworking.ChatMessages.Count > 0)
            {
                foreach (var message in ChatNetworking.ChatMessages)
                {
                    text += message + " \n";
                }
            }

            MessagesText.text = text;
        }

        private void onRecieveMessage(NetworkListEvent<string> changeEvent)
        {
            MessagesText.text += changeEvent.Value + " \n";
        }

        public void SendTextMessage()
        {
            if (!string.IsNullOrEmpty(MessageField.text))
            {
                ChatNetworking.onSendMessage(MessageField.text);
                MessageField.text = "";
            }
        }


    }
}
