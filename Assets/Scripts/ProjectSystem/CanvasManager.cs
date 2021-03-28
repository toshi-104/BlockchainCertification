using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectSystem {
    public class CanvasManager : MonoBehaviour {
        [SerializeField] private List<GameObject> canvases = new List<GameObject>();
        [SerializeField] private GameObject debugCanvas = default;
        private int currentCanvas;

        private int CurrentCanvas {
            get => currentCanvas;
            set => currentCanvas = value < canvases.Count ? value : 0;
        }

        private void Start() {
            foreach (var canvas in canvases) {
                canvas.SetActive(false);
            }

            canvases[0].SetActive(true);
            debugCanvas.SetActive(SettingsSaveSystem.IsDebug());
        }

        public void NextCanvas() {
            canvases[CurrentCanvas].SetActive(false);
            CurrentCanvas++;
            canvases[CurrentCanvas].SetActive(true);
        }

        public void PrevCanvas() {
            canvases[CurrentCanvas].SetActive(false);
            CurrentCanvas--;
            canvases[CurrentCanvas].SetActive(true);
        }

        public void MoveCanvas(GameObject canvas) {
            canvases[CurrentCanvas].SetActive(false);
            CurrentCanvas = canvases.Select((o, i) => new {value = o, index = i})
                .Where(x => x.value == canvas)
                .Select(x => x.index)
                .First();
            canvases[CurrentCanvas].SetActive(true);
        }
    }
}
