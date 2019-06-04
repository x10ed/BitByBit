using System.Collections;
using UnityEngine;

namespace Assets.Scripts {
    public class SizeGate : BaseGate {

        public AssemblyCSharp.BallSizeEnum ToSize = AssemblyCSharp.BallSizeEnum.Normal;
        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        private bool isDragged;
        float clickStart = 0;
        private Vector3 currentPosition;
        void OnMouseDown(){
            isDragged = false;
            clickStart = Time.time;
            currentPosition = transform.position;

        }
	
        void OnMouseUp(){
            if (!isDragged) {
                if ((Time.time - clickStart) < 0.7f) {
                    this.OnClick();
                    clickStart = -1;
                }
            }
        }
        void OnMouseDrag(){
            isDragged =  !CompareApproximate(currentPosition,transform.position);
        }

        public bool CompareApproximate(Vector3 a, Vector3 b)
        {
            if(!Mathf.Approximately(a.x, b.x))
                return false;
            if(!Mathf.Approximately(a.y, b.y))
                return false;
            return true;
        }


        void OnClick(){
            if (ToSize == AssemblyCSharp.BallSizeEnum.Large) {
                ToSize = AssemblyCSharp.BallSizeEnum.Small;
            }
            else if (ToSize == AssemblyCSharp.BallSizeEnum.Small) {
                ToSize = AssemblyCSharp.BallSizeEnum.Normal;
            }
            else if (ToSize == AssemblyCSharp.BallSizeEnum.Normal) {
                ToSize = AssemblyCSharp.BallSizeEnum.Large;
            }
        }
    }
}