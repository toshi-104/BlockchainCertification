using QrCode;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Test.QrCode {
    public class ReadQrText : MonoBehaviour {
        private void Start() {
            var text = GetComponent<Text>();
            WebCamTexture texture = null;

            WebCam.WebCamChanged
                .Subscribe(x => { texture = x; })
                .AddTo(this);

            Observable.EveryFixedUpdate()
                .Where(_ => texture != null)
                .Subscribe(_ => text.text = QrCodeSystem.ReadQrCode(texture))
                .AddTo(this);
        }
    }
}
