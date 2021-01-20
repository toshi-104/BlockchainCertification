﻿using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Verifier;

namespace UI.Verifier {
    public class VerifyButton : MonoBehaviour {
        [SerializeField] private VerifierManager manager = default;

        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => manager.Verify().Forget())
                .AddTo(this);
        }
    }
}