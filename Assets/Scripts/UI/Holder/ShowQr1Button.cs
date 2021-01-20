using Holder;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Holder {
    public class DisplayQr1Button : MonoBehaviour {
        [SerializeField] private HolderManager manager = default;

        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => manager.DisplayQr1())
                .AddTo(this);
        }
    }
}
