using System;
using Credential;
using Holder;
using ProjectSystem;
using QrCode;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Holder {
    public class ShowVerifyQrImage : MonoBehaviour {
        [Inject] private HolderManager holderManager = default;

        private void OnEnable() {
            var rawImage = GetComponent<RawImage>();
            var id = Hash.GetHash(PhoneId.GetImei() + holderManager.HolderName + holderManager.TimeStamp);

            rawImage.texture = QrCodeSystem.CreateQrCode(id + "," + PhoneId.GetImei() + "," + holderManager.HolderName);
        }
    }
}
