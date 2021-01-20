using QrCode;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Test.QrCode {
    public class StartWebCamButton : MonoBehaviour {
        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => WebCam.StartWebCam())
                .AddTo(this);
        }
    }
}
