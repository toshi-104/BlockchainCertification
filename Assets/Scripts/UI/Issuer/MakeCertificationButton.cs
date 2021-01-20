using Issuer;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Issuer {
    public class MakeCertificationButton : MonoBehaviour {
        [SerializeField] private IssuerManager manager = default;

        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => manager.CreateCertification())
                .AddTo(this);
        }
    }
}
