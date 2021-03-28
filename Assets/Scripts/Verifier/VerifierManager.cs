using System.Collections.Generic;
using Credential;
using UnityEngine;

namespace Verifier {
    public class VerifierManager : MonoBehaviour {
        public string CertificateId { get; private set; }
        public string ClientId { get; private set; }
        public string HolderName { get; private set; }
        public string IdPhoto { get; private set; }
        public string Hash { get; private set; }
        public string IssueDate { get; private set; }
        public string Category { get; private set; }
        public string Result { get; private set; }

        public void SetMessage(string message) {
            var s = message.Split(',');
            IdPhoto = s[0];
            Hash = s[1];
        }

        public void OnQrScanned(string scanData) {
            var split = scanData.Split(',');
            CertificateId = split[0];
            ClientId = split[1];
            HolderName = split[2];
        }

        public void SetCertification(Dictionary<string, object> certification) {
            (IssueDate, Category, Result) = Certificate.GetData(certification);
        }
    }
}
