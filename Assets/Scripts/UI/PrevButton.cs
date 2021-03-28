using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI {
    public class PrevButton : MonoBehaviour {
        [Inject] private CanvasManager canvasManager = default;

        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => canvasManager.PrevCanvas())
                .AddTo(this);
        }
    }
}
