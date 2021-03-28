using System;
using Credential;
using Holder;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Holder {
    public class ShowIdPhoto2 : MonoBehaviour {
        [Inject] private HolderManager holderManager = default;

        private void Start() {
            var rawImage = GetComponent<RawImage>();

            holderManager.IdPhoto
                .Where(x => x != null)
                .Subscribe(x => rawImage.texture = IdPhoto.GetPhoto(x))
                .AddTo(this);
        }
    }
}
