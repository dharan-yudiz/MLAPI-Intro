
using MLAPI;
using MLAPI.NetworkVariable.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace MLAPI.Demo
{
    public class Chat : NetworkBehaviour
    {
        private NetworkList<string> ChatMessages = new NetworkList<string>(new NetworkVariable.NetworkVariableSettings
        {
            ReadPermission = MLAPI.NetworkVariable.NetworkVariablePermission.Everyone,
            WritePermission = MLAPI.NetworkVariable.NetworkVariablePermission.Everyone,
            SendTickrate = 5

        }, new List<string>());

        private void OnEnable()
        {
            PopulateList();
            ChatMessages.OnListChanged += onRecieveMessage;
        }

        private void OnDisable()
        {
            ChatMessages.OnListChanged -= onRecieveMessage;
        }

        private void PopulateList()
        {
            throw new NotImplementedException();
        }

        private void onRecieveMessage(NetworkListEvent<string> changeEvent)
        {
            throw new NotImplementedException();
        }

        public void onSendMessage(string Text)
        {
            ChatMessages.Add(Text);
        }
    }
}