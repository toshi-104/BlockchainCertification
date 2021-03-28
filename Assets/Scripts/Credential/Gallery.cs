using UnityEngine;

namespace Credential {
    public static class Gallery {
#if UNITY_ANDROID
        private static readonly AndroidJavaObject AndroidGallery = new AndroidJavaObject("credential.AndroidGallery");
#endif

        public static void ShowGallery() {
#if UNITY_ANDROID
            AndroidGallery.Call("openGallery");
#endif
        }
    }
}
