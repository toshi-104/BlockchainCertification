using Bluetooth;
using Credential;
using Holder;
using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Holder {
    public class SendIssuerButton : MonoBehaviour {
        [Inject] private HolderManager holderManager = default;

        private void Start() {
            var button = GetComponent<Button>();
            Debug.Log(holderManager.IdPhoto);

            button.OnClickAsObservable()
                .Subscribe(_ => {
                    BluetoothSystem.Client();
                    BluetoothSystem.Send(holderManager.IdPhoto.Value);
                })
                .AddTo(this);
        }
    }
}
