using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Setting {
    public class ConnectPortInputField : MonoBehaviour {
        private void Start() {
            var inputField = GetComponent<InputField>();

            var defaultPort = SettingsSaveSystem.GetConnectPort();
            inputField.placeholder.GetComponent<Text>().text = defaultPort.ToString();

            inputField.OnEndEditAsObservable()
                .Select(x => int.TryParse(x, out var i) ? i : defaultPort)
                .Subscribe(SettingsSaveSystem.SaveConnectPort)
                .AddTo(this);
        }
    }
}
