using UnityEngine;
using System.Collections;
using AssemblyCSharp;

namespace Assets.Scripts {
	public class Jump : BaseGate {
		
		float jumpDistanceLeftRight = 4;	
		float jumpDistanceUpDown = 3.4f;	
		public bool IsNeutral;

		public Vector3 GetNewPosition(DirectionEnum currentDirection, Vector3 position){

			switch(currentDirection) {
				case DirectionEnum.Right: // Paremale
					position.x += jumpDistanceLeftRight;
					break;
				case DirectionEnum.Down: // Alla
					position.y -= jumpDistanceUpDown;
					break;
				case DirectionEnum.Left: // Vasakule
					position.x -= jumpDistanceLeftRight;
					break;
				case DirectionEnum.Up: // Üles
					position.y += jumpDistanceUpDown;
					break;
			}
			return position;
		}
	}
}
