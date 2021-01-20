using System.Collections.Generic;
using UnityEngine;

namespace Bluetooth {
    public static class BluetoothSystem {
        public static string Device { private get; set; }
#if UNITY_ANDROID
        private static readonly AndroidJavaObject AndroidBluetoothSystem =
            new AndroidJavaObject("bluetooth.android.AndroidBluetoothSystem");
#endif

        public static IEnumerable<string> GetDevices() {
#if UNITY_ANDROID
            return AndroidBluetoothSystem.Call<string[]>("GetDevices");
#endif
            return new string[0];
        }

        public static void Server() {
#if UNITY_ANDROID
            AndroidBluetoothSystem.Call("Server");
#endif
        }

        public static void Client() {
#if UNITY_ANDROID
            AndroidBluetoothSystem.Call("Client", Device);
#endif
        }

        public static void Send(string message) {
#if UNITY_ANDROID
            AndroidBluetoothSystem.Call("Send", message);
#endif
        }

        public static void Receive(string gameObject, string method) {
#if UNITY_ANDROID
            AndroidBluetoothSystem.Call("Receive", gameObject, method);
#endif
        }
    }
}
