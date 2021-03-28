using Credential;
using Issuer;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Issuer {
    public class ShowIdPhoto : MonoBehaviour {
        [Inject] private IssuerManager issuerManager = default;

        private void OnEnable() {
            var rawImage = GetComponent<RawImage>();

            rawImage.texture = IdPhoto.GetPhoto(issuerManager.IdPhoto);
        }
    }
}
