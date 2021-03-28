using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using BlockChainClient.P2P;
using Cysharp.Threading.Tasks;
using ProjectSystem;

namespace BlockChainClient.Core {
    public static class ClientCore {
        private static ConnectionManager4Edge cm;
        private static string myCoreHost;
        private static int myCorePort;

        static ClientCore() {
            Debugger.Log("Initializing ClientCore...");
            var myIp = GetMyIp();
            Debugger.Log("Client IP address is set to ..." + myIp);
            myCoreHost = SettingsSaveSystem.GetConnectIP();
            myCorePort = SettingsSaveSystem.GetConnectPort();
            cm = new ConnectionManager4Edge(myIp, SettingsSaveSystem.GetHostPort(), myCoreHost, myCorePort);
        }

        public static void Start() {
            cm.ConnectToCoreNode();
        }

        public static void SendMessageToMyCoreNode(MsgType msgType, string msg) {
            var msgTxt = cm.GetMessageText(msgType, msg);
            Debugger.Log(msgTxt);
            ConnectionManager4Edge.SendMsg(new IPEndPoint(IPAddress.Parse(myCoreHost), myCorePort), msgTxt);
        }

        public static UniTask<Dictionary<string, object>> ReceiveCertification() {
            return cm.ReceiveCertification();
        }

        private static IPAddress GetMyIp() {
            var s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s.Connect(new IPEndPoint(IPAddress.Parse("8.8.8.8"), 80));
            var ipEndPoint = (IPEndPoint) s.LocalEndPoint;
            return ipEndPoint.Address;
        }
    }
}
