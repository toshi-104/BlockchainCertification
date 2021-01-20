using System.Collections.Generic;
using Bluetooth;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Holder {
    public class ShowPairingDevices : MonoBehaviour {
        private void Start() {
            var dropdown = GetComponent<Dropdown>();
            var devices = new List<string>() {"選択してください"};
            devices.AddRange(BluetoothSystem.GetDevices());
            dropdown.ClearOptions();
            dropdown.AddOptions(devices);
            dropdown.RefreshShownValue();
            dropdown.OnValueChangedAsObservable()
                .Subscribe(x => BluetoothSystem.Device = devices[x])
                .AddTo(this);
        }
    }
}
