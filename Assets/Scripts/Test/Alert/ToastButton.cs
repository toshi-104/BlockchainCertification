using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Alert {
    public class ToastButton : MonoBehaviour {
        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => ProjectSystem.NotificationSystem.ShowShortToast("Hello World"));
        }
    }
}
