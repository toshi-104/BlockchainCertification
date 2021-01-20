using QrCode;
using UnityEngine;

namespace Test.QrCode {
    public class QrCodeTest : MonoBehaviour {
        private void Start() {
            var texture = QrCodeSystem.CreateQrCode("こんにちは、世界");

            GetComponent<Renderer>().material.mainTexture = texture;
        }
    }
}
