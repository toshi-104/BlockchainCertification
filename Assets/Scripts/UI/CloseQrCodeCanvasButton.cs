using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class CloseQrCodeCanvasButton : MonoBehaviour {
        [SerializeField] private GameObject qrCodeCanvas = default;

        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => qrCodeCanvas.SetActive(false))
                .AddTo(this);
        }
    }
}
