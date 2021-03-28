using UnityEngine;

namespace ProjectSystem {
    public static class PhoneId {
#if UNITY_ANDROID
        private static readonly AndroidJavaObject
            AndroidPhoneId = new AndroidJavaObject("projectSystem.AndroidPhoneId");
#endif

        public static string GetImei() {
#if UNITY_ANDROID
            return AndroidPhoneId.Call<string>("getImei");
#endif
            return null;
        }
    }
}
