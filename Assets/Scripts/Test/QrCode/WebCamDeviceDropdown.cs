using System.Linq;
using QrCode;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Test.QrCode {
    public class WebCamDeviceDropdown : MonoBehaviour {
        private void Start() {
            var dropdown = GetComponent<Dropdown>();

            var deviceNames = WebCam.Devices.Select(x => x.name).ToList();
            dropdown.AddOptions(deviceNames);
            dropdown.RefreshShownValue();

            dropdown.OnValueChangedAsObservable()
                .Subscribe(x => WebCam.ChangeWebCam(WebCam.Devices[x]))
                .AddTo(this);
        }
    }
}
