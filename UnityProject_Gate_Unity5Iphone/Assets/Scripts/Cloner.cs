using AssemblyCSharp;
using UnityEngine;

namespace Assets.Scripts {
    public class Cloner : MonoBehaviour {

        public ColorEnum CloneColor;

        void OnTriggerExit(Collider other){
            var ball = other.GetComponent<Ball>();

            if (ball == null || !ball.go)
                return;

            if (ball.Color == CloneColor && ball.transform.tag != "Ball_Clone") {
                var newPos = this.transform.position;
                newPos.z = ball.transform.position.z; // projektiili puhul z jätame samaks
                var clone = Instantiate (ball, newPos, ball.transform.rotation) as Ball;
                if (clone == null)
                    return;

                //Ball ballComponent = clone as Ball;
                clone.tag = "Ball_Clone";		
                var name = clone.GetComponent<Renderer>().material.name;
                if (clone != null){
                    clone.Rotate(ball.CurrentDirection);}				
            } else if (ball.transform.tag == "Ball_Clone") {
                ball.tag = "Ball";
            }
        }
    }
}
