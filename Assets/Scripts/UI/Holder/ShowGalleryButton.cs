using Credential;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Holder {
    public class ShowGalleryButton : MonoBehaviour {
        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => Gallery.ShowGallery())
                .AddTo(this);
        }
    }
}
