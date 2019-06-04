using System;
using AssemblyCSharp;
using Assets.Scripts;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class GunImmediately : MonoBehaviour,IReset {
	public GameObject projectile;
	public Vector3 position;
	//int Projectile1CurrentCount;
	//public int Projectile1InitialCount;

	public AssemblyCSharp.DirectionEnum direction;
	int temp_debugNumber = 0 ;

    void Start() {	
		Reset ();
	}

	void Fire(){
		GameObject clone;
		
		if (projectile != null){
			
			var newPos = this.transform.position;
			newPos.z = position.z;
			newPos.y += position.y;
			newPos.x += position.x;
			
			clone = Instantiate(projectile, newPos, transform.rotation) as GameObject;
			clone.name = clone.name + temp_debugNumber++;
			var nameVar = clone.GetComponent<Renderer>().material.name; // cloonimise unity kiiks - kui materjali ei küsi siis seda ei cloonita 
			clone.tag = "Ball";
			Ball ballComponent = clone.GetComponent(typeof(Ball)) as Ball;
			if (ballComponent != null){
				ballComponent.Rotate(direction);
			}
		}
    
	}
	
	public void Reset(){
		Fire ();	
	}
}
