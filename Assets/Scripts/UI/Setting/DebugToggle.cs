using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Setting {
    public class DebugToggle : MonoBehaviour {
        private void Start() {
            var toggle = GetComponent<Toggle>();

            toggle.isOn = SettingsSaveSystem.IsDebug();

            toggle.OnValueChangedAsObservable()
                .Subscribe(SettingsSaveSystem.SaveDebug)
                .AddTo(this);
        }
    }
}
