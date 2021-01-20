using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectSystem;

namespace BlockChainClient.P2P {
    public class ConnectionManager4Edge {
        private readonly MessageManager mm;
        private readonly string myCoreHost;
        private readonly int myCorePort;
        private readonly int port;
        private readonly IPAddress host;
        private CoreNodeList coreNodeSet = new CoreNodeList();

        public ConnectionManager4Edge(IPAddress host, int myPort, string myCoreHost, int myCorePort) {
            Debugger.Log("initializing ConnectionManager4Edge...");
            this.host = host;
            port = myPort;
            this.myCoreHost = myCoreHost;
            this.myCorePort = myCorePort;
            mm = new MessageManager();
        }

        public void Start() {
            WaitForAccess().Forget();
        }

        /// <summary>
        /// 指定したメッセージ種別のプロトコルメッセージを作成して返却する
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public string GetMessageText(MsgType msgType, string payload = null) {
            var msgTxt = mm.Build(msgType, port, payload);
            Debugger.Log("generated_msg" + msgTxt);
            return msgTxt;
        }

        /// <summary>
        /// 指定されたノードに対してメッセージを送信する
        /// </summary>
        public static void SendMsg(EndPoint peer, string msg) {
            if (SettingsSaveSystem.IsDebug()) {
                Debugger.Log("Sending..." + msg);
            }

            try {
                var s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Connect(peer);
                s.Send(Encoding.UTF8.GetBytes(msg));
                s.Close();
            }
            catch (Exception e) {
                // TODO ノードに接続できなかったときの処理
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// ユーザが指定した既知のCoreノードへの接続
        /// </summary>
        public void ConnectToCoreNode() {
            ConnectToP2Pnw(myCoreHost, myCorePort);
        }

        public async UniTask<Dictionary<string, object>> ReceiveCertification() {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(host, port));
            socket.Listen(0);

            var soc = await UniTask.Run(() => socket.Accept());
            var ep = (IPEndPoint) soc.RemoteEndPoint;

            var bytes = new byte[1024];
            var data = soc.Receive(bytes);
            var dataSum = Encoding.UTF8.GetString(bytes, 0, data);
            var (result, reason, cmd, peerPort, payload) = mm.Parse(dataSum);

            return cmd == MsgType.Enhanced ? MyProtocolMessageHandler.HandleMessage(payload.ToString()) : null;
        }

        /// <summary>
        /// 指定したCoreノードへ接続要求メッセージを送信する
        /// </summary>
        private void ConnectToP2Pnw(string host, int port) {
            var s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(new IPEndPoint(IPAddress.Parse(host), port));
            var msg = mm.Build(MsgType.AddAsEdge, this.port);
            Debugger.Log(msg);
            s.Send(Encoding.UTF8.GetBytes(msg));
            s.Close();
        }

        /// <summary>
        /// Serverソケットを開いて待ち受け状態に移行する
        /// </summary>
        private async UniTaskVoid WaitForAccess() {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(host, port));
            socket.Listen(0);


            ThreadPool.SetMaxThreads(10, 10);

            while (true) {
                Debugger.Log("Waiting for the connection...");
                var soc = await UniTask.Run(() => socket.Accept());
                var ep = (IPEndPoint) soc.RemoteEndPoint;
                Debugger.Log("Connected by ..." + ep.Address);
                HandleMessage(soc);
            }
        }

        /// <summary>
        /// 受信したメッセージを確認して、内容に応じた処理を行う。
        /// </summary>
        private void HandleMessage(Socket soc) {
            var bytes = new byte[1024];
            var data = soc.Receive(bytes);
            var dataSum = Encoding.UTF8.GetString(bytes, 0, data);
            var (result, reason, cmd, peerPort, payload) = mm.Parse(dataSum);
            Debugger.Log(result + reason + cmd + peerPort + payload);
            var status = (result, reason);

            if (status == ("error", ReasonType.ErrProtocolUnmatch)) {
                Debugger.Log("Error: Protocol name is not matched");
                return;
            }
            else if (status == ("error", ReasonType.ErrVersionUnmatch)) {
                Debugger.Log("Error: Protocol version is not matched");
            }
            else if (status == ("ok", ReasonType.OkWithoutPayload)) {
                if (cmd != MsgType.Ping) {
                    Debugger.Log("Edge node does not have functions for this message!");
                }
            }
            else if (status == ("ok", ReasonType.OkWithPayload)) {
                switch (cmd) {
                    case MsgType.CoreList:
                        // todo: python独自のpickleが送られてくるから処理が面倒
                        break;
                    case MsgType.Enhanced:
                        Debugger.Log(payload.GetType());
                        MyProtocolMessageHandler.HandleMessage(payload.ToString());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else {
                Debugger.Log("Unexpected status" + status);
            }
        }
    }
}
