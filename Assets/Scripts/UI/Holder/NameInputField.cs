using Holder;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Holder {
    public class NameInputField : MonoBehaviour {
        [Inject] private HolderManager holderManager = default;

        private void Start() {
            var inputField = GetComponent<InputField>();

            inputField.OnEndEditAsObservable()
                .Subscribe(x => holderManager.SetHolderName(x))
                .AddTo(this);
        }
    }
}
