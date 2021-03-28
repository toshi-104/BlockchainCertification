using UnityEngine;
using UnityEngine.UI;
using Verifier;
using Zenject;

namespace UI.Verifier {
    public class ShowHolderName : MonoBehaviour {
        [Inject] private VerifierManager verifierManager = default;

        private void OnEnable() {
            var text = GetComponent<Text>();
            text.text = verifierManager.HolderName + "さん";
        }
    }
}
