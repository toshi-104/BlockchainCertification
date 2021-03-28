using System;
using BlockChainClient.Core;
using BlockChainClient.P2P;
using Credential;
using ProjectSystem;
using UnityEngine;
using Zenject;

namespace Issuer {
    public class Qr1Canvas : MonoBehaviour {
        [Inject] private IssuerManager issuerManager = default;

        private void OnEnable() {
            issuerManager.SetTimeStamp(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            var id = Hash.GetCertificateId(PhoneId.GetImei(), issuerManager.HolderName, issuerManager.TimeStamp);
            Debugger.Log(id);
            var certificate = Certificate.CreateCertificate(id, issuerManager.IssuerName, issuerManager.TimeStamp,
                issuerManager.Category, issuerManager.Result);
            ClientCore.Start();
            ClientCore.SendMessageToMyCoreNode(MsgType.NewTransaction, certificate);
        }
    }
}
