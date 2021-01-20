using Bluetooth;
using ProjectSystem;
using QrCode;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Holder {
    public class HolderManager : MonoBehaviour {
        [SerializeField] private GameObject cameraCanvas = default;
        [SerializeField] private GameObject qrCanvas = default;
        [SerializeField] private RawImage qrCodeImage = default;
        [SerializeField] private InputField inputField = default;
        private WebCamTexture texture;
        private string id = "";

        private void Start() {
            WebCam.WebCamChanged
                .Subscribe(x => texture = x)
                .AddTo(this);
        }

        public void DisplayQr1() {
            qrCanvas.SetActive(true);
            qrCodeImage.texture = QrCodeSystem.CreateQrCode(PhoneId.GetImei() + "," + inputField.text);
        }

        public void DisplayQr2() {
            qrCanvas.SetActive(true);
            qrCodeImage.texture = QrCodeSystem.CreateQrCode(id);
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
                        NotificationSystem.ShowShortToast(id);
                    }
                })
                .AddTo(this);
        }

        public void SendPersonalData() {
            BluetoothSystem.Client();
            BluetoothSystem.Send(PhoneId.GetImei() + "," + inputField.text);
        }
    }
}
