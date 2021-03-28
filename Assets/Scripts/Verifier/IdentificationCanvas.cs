using BlockChainClient.Core;
using BlockChainClient.P2P;
using Credential;
using Cysharp.Threading.Tasks;
using ProjectSystem;
using UnityEngine;
using Zenject;

namespace Verifier {
    public class IdentificationCanvas : MonoBehaviour {
        [Inject] private VerifierManager verifierManager = default;

        private void OnEnable() {
            Verify().Forget();
        }

        private async UniTaskVoid Verify() {
            ClientCore.Start();
            ClientCore.SendMessageToMyCoreNode(MsgType.Enhanced, verifierManager.CertificateId);
            var certification = await ClientCore.ReceiveCertification();
            verifierManager.SetCertification(certification);
            var hash = Hash.GetHash(verifierManager.CertificateId + verifierManager.HolderName +
                                    verifierManager.IssueDate);

            if (hash != verifierManager.Hash) {
                NotificationSystem.ShowDialog("エラー", "IDが違います", "ok");
            }
        }
    }
}
