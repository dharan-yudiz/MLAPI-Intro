using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLAPI.Demo
{

    public class GameManager : MonoBehaviour
    {
        static GameManager _instance;
        public static GameManager Instance => _instance;

        public PlayerData currentPlayerData;
        public int Score;

        private void Awake()
        {
            _instance = this;
        }


    }

}