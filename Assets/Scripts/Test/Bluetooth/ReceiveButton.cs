using System;
using Bluetooth;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Bluetooth {
    public class ReceiveButton : MonoBehaviour {
        [SerializeField] private DisplayReceiveText displayReceiveText;

        private void Start() {
            var button = GetComponent<Button>();
            var gameObjectName = displayReceiveText.gameObject.name;
            var methodName = ((Action<string>) displayReceiveText.DisplayText).Method.Name;

            button.OnClickAsObservable()
                .Subscribe(_ => BluetoothSystem.Receive(gameObjectName, methodName))
                .AddTo(this);
        }
    }
}
