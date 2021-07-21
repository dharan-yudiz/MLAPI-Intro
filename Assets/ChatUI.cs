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
    public class ChatUI : MonoBehaviour
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
                    text += "<b>" + message.Playername + "</b> : " + message.MessageText + " \n";
                }
            }

            MessagesText.text = text;
        }

        private void onRecieveMessage(NetworkListEvent<Message> changeEvent)
        {
            MessagesText.text += "<b>" + changeEvent.Value.Playername + "</b> : " + changeEvent.Value.MessageText + " \n";
        }

        [ServerRpc]
        public void SendTextMessageServerRpc()
        {
            if (!string.IsNullOrEmpty(MessageField.text))
            {
                ChatNetworking.onSendMessage(MessageField.text);
                MessageField.text = "";
            }
        }


    }
}
