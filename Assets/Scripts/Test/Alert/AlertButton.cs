using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Alert {
    public class AlertButton : MonoBehaviour {
        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => ProjectSystem.NotificationSystem.ShowDialog("Test", "Hello World", "Hi"))
                .AddTo(this);
        }
    }
}
