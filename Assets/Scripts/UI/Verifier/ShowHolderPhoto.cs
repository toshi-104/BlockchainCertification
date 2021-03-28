using Credential;
using UnityEngine;
using UnityEngine.UI;
using Verifier;
using Zenject;

namespace UI.Verifier {
    public class ShowHolderPhoto : MonoBehaviour {
        [Inject] private VerifierManager verifierManager = default;

        private void OnEnable() {
            var rawImage = GetComponent<RawImage>();

            rawImage.texture = IdPhoto.GetPhoto(verifierManager.IdPhoto);
        }
    }
}
