using UnityEngine;

namespace Assets.Scripts {
    public class BtnAnimation : Disableable {

        private Vector3 OriginalSize;
        public float sizeMultiplier;

        void OnMouseDown(){
            if (IsDisabled) {
                return;
            }
            OriginalSize = this.transform.localScale;
            this.transform.localScale = new Vector3 (transform.localScale.x * sizeMultiplier, transform.localScale.y * sizeMultiplier, transform.localScale.z);
        }

        void OnMouseUp(){
            if (IsDisabled) {
                return;
            }
            this.transform.localScale = OriginalSize;
        }
    }
}
