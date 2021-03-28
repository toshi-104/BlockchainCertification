using UnityEngine;

namespace ProjectSystem {
    public static class NotificationSystem {
#if UNITY_ANDROID
        private static readonly AndroidJavaObject AndroidNotification =
            new AndroidJavaObject("projectSystem.AndroidNotification");
#endif

        public static void ShowShortToast(string message) {
#if UNITY_ANDROID
            AndroidNotification.Call("showShortToast", message);
#endif
        }

        public static void ShowLongToast(string message) {
#if UNITY_ANDROID
            AndroidNotification.Call("showLongToast", message);
#endif
        }

        public static void ShowDialog(string title, string message, string buttonText) {
#if UNITY_ANDROID
            AndroidNotification.Call("showDialog", title, message, buttonText);
#endif
        }

        public static void ShowProgressDialog(string message) {
#if UNITY_ANDROID
            AndroidNotification.Call("showProgressDialog", message);
#endif
        }

        public static void HideProgressDialog() {
#if UNITY_ANDROID
            AndroidNotification.Call("hideProgressDialog");
#endif
        }
    }
}
