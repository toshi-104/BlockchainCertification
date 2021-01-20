using Bluetooth;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Bluetooth {
    public class ConnectButton : MonoBehaviour {
        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => BluetoothSystem.Client())
                .AddTo(this);
        }
    }
}
