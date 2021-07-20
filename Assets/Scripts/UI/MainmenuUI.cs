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
            CustomNetworkManager.Instance.Host(NameField.text, PasswordField.text);
        }

        void OnJoin()
        {
            CustomNetworkManager.Instance.Client(new ConnectionPayload(NameField.text, PasswordField.text));

   
        }
    }
}