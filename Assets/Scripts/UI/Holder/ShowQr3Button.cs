using Holder;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Holder {
    public class DisplayQr3Button : MonoBehaviour {
        [SerializeField] private HolderManager manager = default;

        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => manager.DisplayQr2())
                .AddTo(this);
        }
    }
}
