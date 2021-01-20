using System;
using System.Collections.Generic;
using MiniJSON;
using ProjectSystem;

namespace BlockChainClient.P2P {
    public class MessageManager {
        private const string ProtocolName = "simple_bitcoin_protocol";
        private readonly Version myVersion = new Version("0.1.0");

        public MessageManager() {
            Debugger.Log("Initializing MessageManager...");
        }

        /// <summary>
        /// プロトコルメッセージの組み立て
        /// </summary>
        public string Build(MsgType msgType, int myPort = 50082, string payload = null) {
            var message = new Dictionary<string, object>() {
                {"protocol", ProtocolName},
                {"version", myVersion},
                {"msg_type", (int) msgType},
                {"my_port", myPort}
            };

            if (payload != null) {
                message["payload"] = payload;
            }

            return Json.Serialize(message);
        }

        /// <summary>
        /// プロトコルメッセージをパースして返却する
        /// </summary>
        /// <param name="message">JSON形式のプロトコルメッセージデータ</param>
        /// <returns>結果とパース結果の種別と送信元ポート番号およびペーロードのデータ</returns>
        public (string, ReasonType, MsgType?, int?, object) Parse(string message) {
            var msg = Json.Deserialize(message) as Dictionary<string, object>;
            var msgVer = msg.ContainsKey("version") ? new Version(msg["version"].ToString()) : null;

            var cmd = msg.ContainsKey("msg_type")
                ? (MsgType) Enum.ToObject(typeof(MsgType), msg["msg_type"])
                : (MsgType?) null;
            var myPort = msg.ContainsKey("msg_type") ? (int) (long) msg["my_port"] : (int?) null;
            var payload = msg.ContainsKey("payload") ? msg["payload"] : null;

            if (msg["protocol"].ToString() != ProtocolName) {
                return ("error", ReasonType.ErrProtocolUnmatch, null, null, null);
            }
            else if (msgVer.CompareTo(myVersion) < 0) {
                return ("error", ReasonType.ErrVersionUnmatch, null, null, null);
            }
            else if (cmd == MsgType.CoreList || cmd == MsgType.NewTransaction || cmd == MsgType.NewBlock ||
                     cmd == MsgType.FullChain || cmd == MsgType.Enhanced) {
                return ("ok", ReasonType.OkWithPayload, cmd, (int) myPort, payload);
            }

            return ("ok", ReasonType.OkWithoutPayload, cmd, (int) myPort, null);
        }
    }
}
