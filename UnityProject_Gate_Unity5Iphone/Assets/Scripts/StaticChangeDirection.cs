using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts;

public class StaticChangeDirection : BaseGate {

	public DirectionEnum currentDirection;	
	public bool IsNeutral;	


	public Vector3 GetVectorDirection(){

		var vectorDirection = new Vector3 (0, 1, 0);
		switch (currentDirection)
		{
		case DirectionEnum.Right: // Paremale
			vectorDirection = new Vector3 (1,0,0);
			break;
		case DirectionEnum.Down: // Alla
			vectorDirection = new Vector3 (0,-1,0);
			break;
		case DirectionEnum.Left: // Vasakule
			vectorDirection = new Vector3 (-1,0,0);	
			break;
		default: // Üles
			vectorDirection = new Vector3 (0,1,0);
			break;
		}

		return vectorDirection;
	}
}
