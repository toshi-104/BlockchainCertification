using Issuer;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Issuer {
    public class ResultInputField : MonoBehaviour {
        [Inject] private IssuerManager issuerManager = default;

        private void Start() {
            var inputField = GetComponent<InputField>();

            inputField.OnEndEditAsObservable()
                .Subscribe(x => issuerManager.SetResult(x))
                .AddTo(this);
        }
    }
}
