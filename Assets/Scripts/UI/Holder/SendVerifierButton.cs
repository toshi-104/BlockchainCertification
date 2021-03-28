using Bluetooth;
using Credential;
using Holder;
using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Holder {
    public class SendVerifierButton : MonoBehaviour {
        [Inject] private HolderManager holderManager = default;

        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => {
                    BluetoothSystem.Client();
                    BluetoothSystem.Send(holderManager.IdPhoto + "," +
                                         Hash.GetHash(PhoneId.GetImei() + holderManager.HolderName +
                                                      holderManager.TimeStamp));
                })
                .AddTo(this);
        }
    }
}
