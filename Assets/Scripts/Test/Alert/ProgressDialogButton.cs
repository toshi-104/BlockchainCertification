using System;
using ProjectSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Alert {
    public class ProgressDialogButton : MonoBehaviour {
        private void Start() {
            var button = GetComponent<Button>();

            button.OnClickAsObservable()
                .Subscribe(_ => {
                    NotificationSystem.ShowProgressDialog("Wait");
                    Observable.Timer(TimeSpan.FromSeconds(5))
                        .Subscribe(__ => {
                            Debug.Log("hide");
                            NotificationSystem.HideProgressDialog(); // 閉じません
                        })
                        .AddTo(this);
                })
                .AddTo(this);
        }
    }
}
