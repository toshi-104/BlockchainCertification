using UnityEngine;

namespace ProjectSystem {
    public static class PhoneId {
#if UNITY_ANDROID
        private static readonly AndroidJavaObject
            AndroidPhoneId = new AndroidJavaObject("ProjectSystem.AndroidPhoneId");
#endif

        public static string GetImei() {
#if UNITY_ANDROID
            return AndroidPhoneId.Call<string>("GetImei");
#endif
            return null;
        }
    }
}
