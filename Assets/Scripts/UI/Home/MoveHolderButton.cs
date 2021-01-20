using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Home {
    public class MoveHolderButton : MonoBehaviour {
        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => SceneManager.Move(Scenes.Holder))
                .AddTo(this);
        }
    }
}
