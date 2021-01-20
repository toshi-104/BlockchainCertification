using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace ProjectSystem {
    public class Debugger {
        [Conditional("UNITY_EDITOR")]
        public static void Log(object message) {
            Debug.Log(message);
        }
    }
}
