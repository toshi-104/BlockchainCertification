using Issuer;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Issuer {
    public class CategoryInputField : MonoBehaviour {
        [Inject] private IssuerManager issuerManager = default;

        private void Start() {
            var inputField = GetComponent<InputField>();

            inputField.OnEndEditAsObservable()
                .Subscribe(x => issuerManager.SetCategory(x))
                .AddTo(this);
        }
    }
}
