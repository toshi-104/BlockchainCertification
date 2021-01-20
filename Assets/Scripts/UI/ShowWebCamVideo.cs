using QrCode;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ShowWebCamVideo : MonoBehaviour {
        private void Start() {
            var image = GetComponent<RawImage>();

            WebCam.StartWebCam();
            WebCam.WebCamChanged
                .Subscribe(x => image.texture = x)
                .AddTo(this);
        }
    }
}
