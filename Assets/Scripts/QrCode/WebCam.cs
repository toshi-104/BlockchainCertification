using System;
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
            SystemPermission();
            Devices = WebCamTexture.devices;
            currentWebCam.Value = new WebCamTexture(Devices[0].name);
        }

        public static void StartWebCam() {
            currentWebCam.Value.Play();
        }

        public static void StopWebCam() {
            currentWebCam.Value.Stop();
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
        private static void SystemPermission() {
#if UNITY_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera)) {
                Permission.RequestUserPermission(Permission.Camera);
            }
#elif UNITY_IOS
            if (!Application.HasUserAuthorization(UserAuthorization.WebCam)) {
                await UniTask.Run(() => Application.RequestUserAuthorization(UserAuthorization.WebCam));
            }
#endif
        }
    }
}
