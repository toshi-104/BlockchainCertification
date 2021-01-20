using System;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSystem {
    /// <summary>
    /// Debug.Log()をUI.Textに表示
    /// </summary>
    public class CatchLog : MonoBehaviour {
        private Text text;
        private StringBuilder builder = new StringBuilder();
        private const bool AutoScroll = true;

        [SerializeField, Tooltip("テキストの先頭に時刻を表示する")]
        private bool useTimeStamp = true;

        [SerializeField, Tooltip("ログの種別に応じて色を付ける")]
        private bool coloredByLogType = true;

        [SerializeField, Tooltip("特定の文字列を含むログは表示しない")]
        private string[] ignore = new string[] {"[OVR"};

        private void Awake() {
            text = this.GetComponent<Text>();
            if (text == null) {
                this.enabled = false;
                throw new NullReferenceException("No text component found.");
            }

            if (AutoScroll)
                text.verticalOverflow = VerticalWrapMode.Truncate;

            if (coloredByLogType)
                text.supportRichText = true;

            text.text = string.Empty;
        }

        private void OnEnable() {
            Application.logMessageReceived += HandleLog;
            builder = new StringBuilder();
        }

        private void OnDisable() {
            Application.logMessageReceived -= HandleLog;
            builder = null;
        }

        private void HandleLog(string logText, string stackTrace, LogType logType) {
            builder.Length = 0;

            if (0 < ignore.Length) {
                if (ignore.Any(s => s != string.Empty && logText.Contains(s))) {
                    return;
                }
            }

            if (useTimeStamp)
                builder.Append($"[{DateTime.Now.ToLongTimeString()}:{DateTime.Now.Millisecond:D3}] ");

            if (coloredByLogType) {
                switch (logType) {
                    case LogType.Assert:
                    case LogType.Warning:
                        logText = GetColoredString(logText, "yellow");
                        break;
                    case LogType.Error:
                    case LogType.Exception:
                        logText = GetColoredString(logText, "red");
                        break;
                    case LogType.Log:
                        break;
                    default:
                        break;
                }
            }

            builder.Append(logText);
            builder.Append(Environment.NewLine);

            text.text += builder.ToString();

            if (AutoScroll && text.verticalOverflow == VerticalWrapMode.Truncate)
                AdjustText(text);
        }

        /// <summary>
        /// 文字列に色付け
        /// </summary>
        /// <param name="src"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private static string GetColoredString(string src, string color) {
            return $"<color={color}>{src}</color>";
        }

        /// <summary>
        /// Textの範囲内に文字列を収める
        /// </summary>
        /// <param name="t"></param>
        private static void AdjustText(Text t) {
            var generator = t.cachedTextGenerator;
            var settings = t.GetGenerationSettings(t.rectTransform.rect.size);
            generator.Populate(t.text, settings);

            var countVisible = generator.characterCountVisible;
            if (countVisible == 0 || t.text.Length <= countVisible)
                return;

            var truncatedCount = t.text.Length - countVisible;
            var lines = t.text.Split('\n');
            foreach (var line in lines) {
                // 見切れている文字数が0になるまで、テキストの先頭行から消してゆく
                t.text = t.text.Remove(0, line.Length + 1);
                truncatedCount -= (line.Length + 1);
                if (truncatedCount <= 0)
                    break;
            }
        }
    }
}
