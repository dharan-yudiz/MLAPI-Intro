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
            CustomNetworkManager.Instance.Password = PasswordField.text;

            CustomNetworkManager.Instance.Host();
        }

        void OnJoin()
        {
            CustomNetworkManager.Instance.Client(PasswordField.text);

   
        }
    }
}