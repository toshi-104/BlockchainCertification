using Issuer;
using QrCode;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Issuer {
    public class ShowQr1RawImage : MonoBehaviour {
        [Inject] private IssuerManager issuerManager = default;

        private void Start() {
            var rawImage = GetComponent<RawImage>();

            rawImage.texture = QrCodeSystem.CreateQrCode(issuerManager.TimeStamp);
        }
    }
}
