using ProjectSystem;
using UnityEngine;

namespace Issuer {
    public class IssuerManager : MonoBehaviour {
        public string ClientId { get; private set; }
        public string HolderName { get; private set; }
        public string IdPhoto { get; private set; }
        public string IssuerName { get; private set; }
        public string Category { get; private set; }
        public string Result { get; private set; }
        public string TimeStamp { get; private set; }

        public void SetPhoto(string photo) {
            Debugger.Log("receive: " + photo);
            IdPhoto += photo;
        }

        public void OnQrScanned(string scanData) {
            var split = scanData.Split(',');
            ClientId = split[0];
            HolderName = split[1];
        }

        public void SetCategory(string category) {
            Category = category;
        }

        public void SetIssuerName(string issuerName) {
            IssuerName = issuerName;
        }

        public void SetResult(string result) {
            Result = result;
        }

        public void SetTimeStamp(string time) {
            TimeStamp = time;
        }
    }
}
