using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Holder {
    public class VerifyButton : MonoBehaviour {
        [SerializeField] private GameObject verifyCanvas = default;
        [Inject] private CanvasManager canvasManager = default;

        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => canvasManager.MoveCanvas(verifyCanvas))
                .AddTo(this);
        }
    }
}
