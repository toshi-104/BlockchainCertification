using System.Net;
using System.Net.Sockets;
using System.Text;
using Cysharp.Threading.Tasks;
using ProjectSystem;
using UnityEngine;

namespace Test.Blockchain {
    public class ServerTest : MonoBehaviour {
        private readonly Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private void Start() {
            UniTask.Run(Wait).Forget();
        }

        private static IPAddress GetMyIp() {
            var s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s.Connect(new IPEndPoint(IPAddress.Parse("8.8.8.8"), 80));
            var ipEndPoint = (IPEndPoint) s.LocalEndPoint;
            return ipEndPoint.Address;
        }

        private UniTaskVoid Wait() {
            this.socket.Bind(new IPEndPoint(GetMyIp(), 50085));
            this.socket.Listen(1);
            while (true) {
                Debugger.Log("Waiting connection...");
                var soc = socket.Accept();
                var ep = (IPEndPoint) soc.RemoteEndPoint;
                var addr = ep.Address;
                Debugger.Log("Connected by " + addr);
                var data = new byte[1024];
                soc.Receive(data);
                Debugger.Log(Encoding.UTF8.GetString(data));
                Debugger.Log("End");
            }
        }

        private void OnApplicationQuit() {
            socket.Close();
            Debugger.Log("socket closed");
        }
    }
}
