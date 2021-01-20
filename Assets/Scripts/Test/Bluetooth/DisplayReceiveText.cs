using UnityEngine;
using UnityEngine.UI;

namespace Test.Bluetooth {
    public class DisplayReceiveText : MonoBehaviour {
        private Text text;

        private void Start() {
            text = GetComponent<Text>();
            text.text = "";
        }

        public void DisplayText(string message) {
            text.text = message;
        }
    }
}
