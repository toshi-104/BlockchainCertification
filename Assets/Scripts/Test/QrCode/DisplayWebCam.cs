using QrCode;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Test.QrCode {
    public class DisplayWebCam : MonoBehaviour {
        private void Start() {
            var rawImage = GetComponent<RawImage>();

            WebCam.WebCamChanged
                .First()
                .Subscribe(_ => transform.rotation = Quaternion.AngleAxis(WebCam.GetDegree(), Vector3.back))
                .AddTo(this);

            WebCam.WebCamChanged
                .Subscribe(x => rawImage.texture = x)
                .AddTo(this);
        }
    }
}
