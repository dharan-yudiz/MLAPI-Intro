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


        [SerializeField] InputField NameField;
        [SerializeField] InputField PasswordField;

        public override void OnScreenShowCalled()
        {
            base.OnScreenShowCalled();
            NameField.text = "";
            PasswordField.text = "";
        }

        public override void OnScreenHideCalled()
        {
            base.OnScreenHideCalled();
            NameField.DeactivateInputField();
            PasswordField.DeactivateInputField();
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
            CustomNetworkManager.Instance.Host(NameField.text, PasswordField.text);
        }

        void OnJoin()
        {
            CustomNetworkManager.Instance.Client(new ConnectionPayload(NameField.text, PasswordField.text));
        }
    }
}