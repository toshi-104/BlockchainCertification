using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Setting {
    public class BackHomeButton : MonoBehaviour {
        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => SceneManager.Move(Scenes.Home))
                .AddTo(this);
        }
    }
}
