using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Android;

#if UNITY_ANDROID

#endif

namespace QrCode {
    public static class WebCam {
        private static ReactiveProperty<WebCamTexture> currentWebCam = new ReactiveProperty<WebCamTexture>();
        public static IObservable<WebCamTexture> WebCamChanged => currentWebCam;
        public static WebCamDevice[] Devices { get; }

        static WebCam() {
            SystemPermission().Forget();
            currentWebCam.Value = new WebCamTexture();
            Devices = WebCamTexture.devices;
        }

        public static void StartWebCam() {
            currentWebCam.Value.Play();
        }

        public static void ChangeWebCam(WebCamDevice device) {
            currentWebCam.Value.Stop();
            currentWebCam.Value = new WebCamTexture(device.name);
            currentWebCam.Value.Play();
        }

        /// <summary>
        /// カメラの取り付け向きを取得
        /// </summary>
        public static int GetDegree() {
            return currentWebCam.Value.videoRotationAngle;
        }

        /// <summary>
        /// カメラへのアクセス許可
        /// </summary>
        private static async UniTaskVoid SystemPermission() {
#if UNITY_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera)) {
                await UniTask.Run(() => Permission.RequestUserPermission(Permission.Camera));
            }
#elif UNITY_IOS
            if (!Application.HasUserAuthorization(UserAuthorization.WebCam)) {
                await UniTask.Run(() => Application.RequestUserAuthorization(UserAuthorization.WebCam));
            }
#endif
        }
    }
}
