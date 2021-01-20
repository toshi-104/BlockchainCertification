using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Home {
    public class MoveIssuerButton : MonoBehaviour {
        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => SceneManager.Move(Scenes.Issuer))
                .AddTo(this);
        }
    }
}
