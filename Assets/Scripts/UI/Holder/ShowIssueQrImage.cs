using Holder;
using ProjectSystem;
using QrCode;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Holder {
    public class ShowIssueQrImage : MonoBehaviour {
        [Inject] private HolderManager holderManager = default;

        private void OnEnable() {
            var rawImage = GetComponent<RawImage>();
            rawImage.texture = QrCodeSystem.CreateQrCode(PhoneId.GetImei() + "," + holderManager.HolderName);
        }
    }
}
