using Debug = UnityEngine.Debug;

namespace ProjectSystem {
    public static class Debugger {
        public static void Log(object message) {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
#if UNITY_ANDROID
            if (SettingsSaveSystem.IsDebug()) {
                Debug.Log(message);
            }
#endif
        }
    }
}
