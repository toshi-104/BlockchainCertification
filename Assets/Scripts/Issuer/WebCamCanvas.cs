using System;
using Bluetooth;
using UnityEngine;
using Zenject;

namespace Issuer {
    public class WebCamCanvas : MonoBehaviour {
        [Inject] private IssuerManager issuerManager = default;

        private void OnEnable() {
            BluetoothSystem.Server();
            BluetoothSystem.Receive(issuerManager.gameObject.name,
                ((Action<string>) issuerManager.SetPhoto).Method.Name);
        }
    }
}
