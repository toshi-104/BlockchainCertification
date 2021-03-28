﻿using Issuer;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Issuer {
    public class ShowHolderName : MonoBehaviour {
        [Inject] private IssuerManager issuerManager = default;

        private void OnEnable() {
            var text = GetComponent<Text>();

            text.text = issuerManager.HolderName + " さん";
        }
    }
}
