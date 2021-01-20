using System.Net;
using System.Net.Sockets;
using System.Text;
using ProjectSystem;
using UnityEngine;

namespace Test.Blockchain {
    public class ClientTest : MonoBehaviour {
        private void Start() {
            var s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(new IPEndPoint(IPAddress.Parse(SettingsSaveSystem.GetConnectIP()), 50082));
            const string msg = "Hello";
            s.Send(Encoding.UTF8.GetBytes(msg));
            s.Close();
        }
    }
}
