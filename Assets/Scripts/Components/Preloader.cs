using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLAPI.Demo {
    public class Preloader : MonoBehaviour {
        private void Awake() {
            string[] args = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++) {
                if (args[i] == "-launch-as-server")
                    CustomNetworkManager.Instance.Server("123");
            }

        }
    }
}