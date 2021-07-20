using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    static CustomNetworkManager _instance;

    List<NetworkObject> _players;
    public List<NetworkObject> Players => _players;

    public static CustomNetworkManager Instance => _instance;


    private void Awake()
    {
        _instance = this;
    }





}
