using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI {
    public class NextButton : MonoBehaviour {
        [Inject] private CanvasManager canvasManager = default;

        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => canvasManager.NextCanvas())
                .AddTo(this);
        }
    }
}
