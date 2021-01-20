using System;
using UnityEngine;

namespace ProjectSystem {
    public static class SettingsSaveSystem {
        private const string HostPort = "hostPort";
        private const string ConnectIP = "connectIP";
        private const string ConnectPort = "connectPort";
        private const string Debug = "Debug";

        public static void SaveHostPort(int port) {
            PlayerPrefs.SetInt(HostPort, port);
            PlayerPrefs.Save();
        }

        public static void SaveConnectIP(string ip) {
            if (ip == "") {
                PlayerPrefs.DeleteKey(ConnectIP);
                return;
            }

            PlayerPrefs.SetString(ConnectIP, ip);
            PlayerPrefs.Save();
        }

        public static void SaveConnectPort(int port) {
            PlayerPrefs.SetInt(ConnectPort, port);
            PlayerPrefs.Save();
        }

        public static void SaveDebug(bool isDebug) {
            PlayerPrefs.SetInt(Debug, Convert.ToInt32(isDebug));
            PlayerPrefs.Save();
        }

        public static int GetHostPort() {
            return PlayerPrefs.HasKey(HostPort) ? PlayerPrefs.GetInt(HostPort) : 50085;
        }

        public static string GetConnectIP() {
            return PlayerPrefs.HasKey(ConnectIP) ? PlayerPrefs.GetString(ConnectIP) : null;
        }

        public static int GetConnectPort() {
            return PlayerPrefs.HasKey(ConnectPort) ? PlayerPrefs.GetInt(ConnectPort) : 50082;
        }

        public static bool IsDebug() {
            return PlayerPrefs.HasKey(Debug) && Convert.ToBoolean(PlayerPrefs.GetInt(Debug));
        }
    }
}
