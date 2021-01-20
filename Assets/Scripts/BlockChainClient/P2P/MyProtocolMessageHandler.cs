using System.Collections.Generic;
using MiniJSON;
using ProjectSystem;

namespace BlockChainClient.P2P {
    public static class MyProtocolMessageHandler {
        /// <summary>
        /// 独自に拡張したENHANCEDメッセージの処理
        /// </summary>
        /// <param name="payload"></param>
        public static Dictionary<string, object> HandleMessage(string payload) {
            return Json.Deserialize(payload) as Dictionary<string, object>;
        }
    }
}
