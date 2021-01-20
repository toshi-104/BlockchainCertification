using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Setting {
    public class ConnectIPInputField : MonoBehaviour {
        private void Start() {
            var inputField = GetComponent<InputField>();

            var defaultIP = SettingsSaveSystem.GetConnectIP();
            inputField.placeholder.GetComponent<Text>().text = defaultIP;

            inputField.OnEndEditAsObservable()
                .Select(x => x != "" ? x : defaultIP)
                .Subscribe(SettingsSaveSystem.SaveConnectIP)
                .AddTo(this);
        }
    }
}
