using System.Collections.Generic;
using MiniJSON;

namespace Credential {
    public static class Certificate {
        public static string CreateCertificate(string id, string issuer, string time, string category, string result) {
            var certificate = new List<Dictionary<string, object>> {
                new Dictionary<string, object> {
                    {"@context", new List<string> {"https://hoge", "https://hoge/hoge"}},
                    {"id", id},
                    {"type", new List<string> {"VerifiableCredential", "AntibodyTestCredential"}},
                    {"issuer", "https://issuer/" + issuer},
                    {"issueDate", time}, {
                        "credentialSubject", new Dictionary<string, object> {
                            {"category", category},
                            {"result", result}
                        }
                    }
                },
                new Dictionary<string, object> {
                    {"proof", null}
                }
            };

            return Json.Serialize(certificate);
        }

        public static (string, string, string) GetData(Dictionary<string, object> certificate) {
            var issueDate = certificate["issueDate"].ToString();
            var subject = certificate["credentialSubject"] as Dictionary<string, object>;
            var category = subject["category"].ToString();
            var result = subject["result"].ToString();
            return (issueDate, category, result);
        }
    }
}
