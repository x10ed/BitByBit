using AssemblyCSharp;
using UnityEngine;

namespace Assets.Scripts {
    public interface IReset {
        void Reset();
    }

    public class FinishCounter : MonoBehaviour, IReset {
        public ColorEnum correctColor;

        public void Reset() {
            UpdateLabel();
        }

        //minuarust on all olev kood üleliigne ja võiks ära kustutada
        //finish loogika on ammu juba balli külle tõstetud
        private void Start() {
            GameObject gameMaster = GameObject.Find("_GameMaster");
            var mainMenuasObject = gameMaster.GetComponent<MainMenu>();

            UpdateLabel();
        }

        private void OnTriggerStay(Collider other) {
            var ball = other.GetComponent<Ball>();
            if (ball == null) {
                return;
            }

            if (!ball.activateBehaviour) {
                return;
            }

            if (IsCorrectObject(other)) {
                if (ball == null)
                    return;
            }
            else {
                return;
            }
            UpdateLabel();
        }

        public bool IsCorrectObject(Collider obj) {
            if (obj == null) {
                return false;
            }
            var ball = obj.gameObject.GetComponent<Ball>();
            return ball != null && ball.Color == correctColor;
        }

        public void UpdateLabel() {
            Transform labelobject = transform.Find("Label"); // rename to 
            if (labelobject == null) {
            }
        }
    }
}