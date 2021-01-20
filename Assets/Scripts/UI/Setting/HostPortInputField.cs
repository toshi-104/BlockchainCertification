using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Setting {
    public class HostPortInputField : MonoBehaviour {
        private void Start() {
            var inputField = GetComponent<InputField>();

            var defaultValue = SettingsSaveSystem.GetHostPort();
            inputField.placeholder.GetComponent<Text>().text = defaultValue.ToString();

            inputField.OnEndEditAsObservable()
                .Select(x => int.TryParse(x, out var i) ? i : defaultValue)
                .Subscribe(SettingsSaveSystem.SaveHostPort)
                .AddTo(this);
        }
    }
}
