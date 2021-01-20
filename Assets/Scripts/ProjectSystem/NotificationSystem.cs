using UnityEngine;

namespace ProjectSystem {
    public static class NotificationSystem {
#if UNITY_ANDROID
        private static readonly AndroidJavaObject AndroidNotification =
            new AndroidJavaObject("ProjectSystem.AndroidNotification");
#endif

        public static void ShowShortToast(string message) {
#if UNITY_ANDROID
            AndroidNotification.Call("ShowShortToast", message);
#endif
        }

        public static void ShowLongToast(string message) {
#if UNITY_ANDROID
            AndroidNotification.Call("ShowLongToast", message);
#endif
        }

        public static void ShowDialog(string title, string message, string buttonText) {
#if UNITY_ANDROID
            AndroidNotification.Call("ShowDialog", title, message, buttonText);
#endif
        }

        public static void ShowProgressDialog(string message) {
#if UNITY_ANDROID
            AndroidNotification.Call("ShowProgressDialog", message);
#endif
        }

        public static void HideProgressDialog() {
#if UNITY_ANDROID
            AndroidNotification.Call("HideProgressDialog");
#endif
        }
    }
}
