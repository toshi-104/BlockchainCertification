using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class CloseWebCamCanvasButton : MonoBehaviour {
        [SerializeField] private GameObject webCamCanvas = default;

        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => webCamCanvas.SetActive(false))
                .AddTo(this);
        }
    }
}
