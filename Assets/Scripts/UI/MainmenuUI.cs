using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MLAPI.Demo
{
    public class MainmenuUI : ScreenView
    {
        [SerializeField] Button btnHost;
        [SerializeField] Button btnJoin;


        [SerializeField] InputField PasswordField;

        public override void OnScreenShowCalled()
        {
            base.OnScreenShowCalled();
            PasswordField.enabled = true;
            PasswordField.text = "";
        }

        public override void OnScreenHideCalled()
        {
            base.OnScreenHideCalled();
            PasswordField.enabled = false;
        }


        private void OnEnable()
        {
            btnHost.onClick.AddListener(OnHost);
            btnJoin.onClick.AddListener(OnJoin);
        }

        private void OnDisable()
        {
            btnHost.onClick.RemoveAllListeners();
            btnJoin.onClick.RemoveAllListeners();
        }

        void OnHost()
        {
            PasswordField.DeactivateInputField();
            CustomNetworkManager.Instance.Host(PasswordField.text);
        }

        void OnJoin()
        {
            CustomNetworkManager.Instance.Client(new ConnectionPayload(GameManager.Instance.currentPlayerData.PlayerName, PasswordField.text,GameManager.Instance.currentPlayerData.PlayerColor));

   
        }
    }
}