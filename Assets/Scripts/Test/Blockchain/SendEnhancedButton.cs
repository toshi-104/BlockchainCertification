using BlockChainClient.Core;
using BlockChainClient.P2P;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Blockchain {
    public class SendEnhancedButton : MonoBehaviour {
        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => ClientCore.SendMessageToMyCoreNode(MsgType.Enhanced, null))
                .AddTo(this);
        }
    }
}
