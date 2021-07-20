using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MLAPI.Demo
{
    public class CustomNetworkManager : NetworkManager
    {
        static CustomNetworkManager _instance;
        public static CustomNetworkManager Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }


    }
}
