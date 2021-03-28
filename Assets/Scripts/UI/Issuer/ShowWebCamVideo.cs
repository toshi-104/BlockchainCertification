using Issuer;
using ProjectSystem;
using QrCode;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Issuer {
    public class ShowWebCamVideo : MonoBehaviour {
        [Inject] private IssuerManager issuerManager = default;
        [Inject] private CanvasManager canvasManager = default;

        private void OnEnable() {
            var rawImage = GetComponent<RawImage>();
            var texture = new WebCamTexture();

            WebCam.StartWebCam();
            WebCam.WebCamChanged
                .First()
                .Subscribe(_ => transform.rotation = Quaternion.AngleAxis(WebCam.GetDegree(), Vector3.back))
                .AddTo(this);

            WebCam.WebCamChanged
                .Subscribe(x => {
                    texture = x;
                    rawImage.texture = x;
                })
                .AddTo(this);

            Observable.EveryFixedUpdate()
                .TakeWhile(_ => issuerManager.ClientId == null)
                .Subscribe(_ => {
                    var data = QrCodeSystem.ReadQrCode(texture);
                    if (data != null) {
                        issuerManager.OnQrScanned(data);
                        canvasManager.NextCanvas();
                        WebCam.StopWebCam();
                    }
                })
                .AddTo(this);
        }
    }
}
