using UniRx;
using UnityEngine;

namespace Holder {
    public class HolderManager : MonoBehaviour {
        public readonly ReactiveProperty<string> IdPhoto = new ReactiveProperty<string>();
        public string HolderName { get; private set; }
        public string TimeStamp { get; private set; }

        public void SetIdPhoto(string base16) {
            IdPhoto.Value = base16;
        }

        public void SetHolderName(string holderName) {
            HolderName = holderName;
        }

        public void SetTimeStamp(string time) {
            TimeStamp = time;
        }
    }
}
