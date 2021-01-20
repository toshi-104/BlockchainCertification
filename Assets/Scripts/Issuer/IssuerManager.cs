using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BlockChainClient.Core;
using BlockChainClient.P2P;
using Credential;
using MiniJSON;
using ProjectSystem;
using QrCode;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Issuer {
    public class IssuerManager : MonoBehaviour {
        [SerializeField] private GameObject cameraCanvas = default;
        [SerializeField] private GameObject qrCanvas = default;
        [SerializeField] private RawImage qrCodeImage = default;
        [SerializeField] private InputField issuer = default;
        [SerializeField] private InputField content = default;
        private WebCamTexture webCamTexture;
        private string clientId;
        private string holderName;

        private void Start() {
            WebCam.WebCamChanged
                .Subscribe(x => webCamTexture = x)
                .AddTo(this);
        }

        public void ScanQr() {
            cameraCanvas.SetActive(true);
            WebCam.StartWebCam();
            var personalData = "";
            Observable.EveryFixedUpdate()
                .TakeWhile(_ => personalData == "")
                .Subscribe(_ => {
                    personalData = QrCodeSystem.ReadQrCode(webCamTexture);
                    if (personalData != "") {
                        var data = personalData.Split(',');
                        clientId = data[0];
                        holderName = data[1];
                        cameraCanvas.SetActive(false);
                        NotificationSystem.ShowShortToast(holderName);
                    }
                })
                .AddTo(this);
        }

        public void CreateCertification() {
            var timeStamp = DateTime.Now;
            var id = Hash.GetHash(clientId + holderName + timeStamp);

            var certification = new List<Dictionary<string, object>>() {
                new Dictionary<string, object>() {
                    {"@context", new List<string>() {"https://hoge", "https://hoge/hoge"}},
                    {"id", "https://id/" + id},
                    {"type", new List<string>() {"VerifiableCredential", "AntibodyTestCredential"}},
                    {"issuer", "https://issuer/" + issuer.text},
                    {"issuerDate", timeStamp}, {
                        "credentialSubject", new Dictionary<string, object>() {
                            {"content", content.text}
                        }
                    }
                },
                new Dictionary<string, object>() {
                    {"proof", null}
                }
            };

            ClientCore.Start();
            ClientCore.SendMessageToMyCoreNode(MsgType.NewTransaction, Json.Serialize(certification));
            var qrCode = QrCodeSystem.CreateQrCode(id);
            qrCanvas.SetActive(true);
            qrCodeImage.texture = qrCode;
            clientId = "";
            holderName = "";
            content.text = "";
        }
    }
}
