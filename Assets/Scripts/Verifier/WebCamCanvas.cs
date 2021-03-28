using System;
using Bluetooth;
using UnityEngine;
using Zenject;

namespace Verifier {
    public class WebCamCanvas : MonoBehaviour {
        [Inject] private VerifierManager verifierManager = default;

        private void OnEnable() {
            BluetoothSystem.Server();
            BluetoothSystem.Receive(verifierManager.gameObject.name,
                ((Action<string>) verifierManager.SetMessage).Method.Name);
        }
    }
}
