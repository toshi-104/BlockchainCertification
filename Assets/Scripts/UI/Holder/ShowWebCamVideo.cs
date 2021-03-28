using Holder;
using ProjectSystem;
using QrCode;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Holder {
    public class ShowWebCamVideo : MonoBehaviour {
        [Inject] private HolderManager holderManager = default;
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
                .TakeWhile(_ => holderManager.TimeStamp == null)
                .Subscribe(_ => {
                    holderManager.SetTimeStamp(QrCodeSystem.ReadQrCode(texture));
                    if (holderManager.TimeStamp != null) {
                        canvasManager.NextCanvas();
                        WebCam.StopWebCam();
                    }
                })
                .AddTo(this);
        }
    }
}
