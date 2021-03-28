using BlockChainClient.Core;
using BlockChainClient.P2P;
using Cysharp.Threading.Tasks;
using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Blockchain {
    public class SendEnhancedButton : MonoBehaviour {
        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => { V().Forget(); })
                .AddTo(this);
        }

        private async UniTaskVoid V() {
            ClientCore.Start();
            ClientCore.SendMessageToMyCoreNode(MsgType.Enhanced, null);
            var c = await ClientCore.ReceiveCertification();
            Debugger.Log(c);
        }
    }
}
