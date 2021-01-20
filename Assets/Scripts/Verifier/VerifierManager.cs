using System;
using BlockChainClient.Core;
using BlockChainClient.P2P;
using Bluetooth;
using Credential;
using Cysharp.Threading.Tasks;
using ProjectSystem;
using QrCode;
using UniRx;
using UnityEngine;

namespace Verifier {
    public class VerifierManager : MonoBehaviour {
        [SerializeField] private GameObject cameraCanvas = default;
        private WebCamTexture texture;
        private string id = "";
        private string clientId;
        private string holderName;

        private void Start() {
            WebCam.WebCamChanged
                .Subscribe(x => texture = x)
                .AddTo(this);
        }

        public void ScanQr() {
            cameraCanvas.SetActive(true);
            WebCam.StartWebCam();
            Observable.EveryFixedUpdate()
                .TakeWhile(_ => id == "")
                .Subscribe(_ => {
                    id = QrCodeSystem.ReadQrCode(texture);
                    if (id != "") {
                        cameraCanvas.SetActive(false);
                    }
                })
                .AddTo(this);
        }

        public void ReceiveData() {
            BluetoothSystem.Server();
            BluetoothSystem.Receive(gameObject.name, ((Action<string>) SetMessage).Method.Name);
        }

        public async UniTaskVoid Verify() {
            ClientCore.Start();
            ClientCore.SendMessageToMyCoreNode(MsgType.Enhanced, id);
            var certification = await ClientCore.ReceiveCertification();
            var timestamp = (DateTime) certification["issuerDate"];
            var hash = Hash.GetHash(clientId + holderName + timestamp);

            if (hash == id) {
                NotificationSystem.ShowDialog("認証結果", certification["credentialSubject"].ToString(), "ok");
            }
            else {
                NotificationSystem.ShowDialog("エラー", "IDが違います", "ok");
            }
        }

        private void SetMessage(string message) {
            var s = message.Split(',');
            clientId = s[0];
            holderName = s[1];
        }
    }
}
