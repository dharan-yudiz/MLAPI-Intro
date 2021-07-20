using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button host;
    [SerializeField] Button join;
    [SerializeField] Canvas mainMenu;

    private void OnEnable() {
        mainMenu.enabled = true;
        host.onClick.AddListener(OnHost);
        join.onClick.AddListener(OnJoin);
    }

    private void OnDisable() {
        host.onClick.RemoveAllListeners();
        join.onClick.RemoveAllListeners();
    }

    void OnHost() {
        NetworkManager.Singleton.StartHost();
        mainMenu.enabled = false;
    }

    void OnJoin() {
        NetworkManager.Singleton.StartClient();
        mainMenu.enabled = false;
    }
}
