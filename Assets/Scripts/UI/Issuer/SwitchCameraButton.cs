using QrCode;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Issuer {
    public class SwitchCameraButton : MonoBehaviour {
        private void Start() {
            var currentDevice = 0;
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Select(_ => currentDevice < WebCam.Devices.Length ? currentDevice++ : 0)
                .Subscribe(x => WebCam.ChangeWebCam(WebCam.Devices[x]))
                .AddTo(this);
        }
    }
}
