using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MLAPI.Demo
{
    public class MainmenuUI : ScreenView
    {
        [SerializeField] Button host;
        [SerializeField] Button join;

        private void OnEnable()
        {
            host.onClick.AddListener(OnHost);
            join.onClick.AddListener(OnJoin);
        }

        private void OnDisable()
        {
            host.onClick.RemoveAllListeners();
            join.onClick.RemoveAllListeners();
        }

        void OnHost()
        {
            CustomNetworkManager.Instance.StartHost();

            UIController.instance.HideCurrentScreen();
            UIController.instance.ShowThisScreen(ScreenType.GamePlay, EnableDirection.Forward);
        }

        void OnJoin()
        {
            CustomNetworkManager.Instance.StartClient();

            UIController.instance.HideCurrentScreen();
            UIController.instance.ShowThisScreen(ScreenType.GamePlay, EnableDirection.Forward);
        }
    }
}