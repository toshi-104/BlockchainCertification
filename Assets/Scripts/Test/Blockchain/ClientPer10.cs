using System;
using System.Collections.Generic;
using BlockChainClient.Core;
using BlockChainClient.P2P;
using MiniJSON;
using UniRx;
using UnityEngine;

namespace Test.Blockchain {
    public class ClientPer10 : MonoBehaviour {
        private void Start() {
            ClientCore.Start();

            var transaction = new List<Dictionary<string, object>>() {
                new Dictionary<string, object>() {
                    {"@context", new List<string>() {"https://hoge", "https://hoge/hoge"}},
                    {"id", "https://id/huga"},
                    {"type", new List<string>() {"VerifiableCredential", "AntibodyTestCredential"}},
                    {"issuer", "https://issuer/piyo"},
                    {"issuerDate", null},
                    {"credentialSubject", new List<string>() {"did:example:xyzzy", "SARS-CoV-2"}}
                },
                new Dictionary<string, object>() {
                    {"proof", null}
                }
            };

            Observable.Interval(TimeSpan.FromSeconds(10))
                .Subscribe(x => {
                    transaction[0]["issuerDate"] = DateTime.Now;
                    ClientCore.SendMessageToMyCoreNode(MsgType.NewTransaction, Json.Serialize(transaction));
                })
                .AddTo(this);
        }
    }
}
