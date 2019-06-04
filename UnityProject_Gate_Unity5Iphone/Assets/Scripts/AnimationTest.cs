using UnityEngine;
using System.Collections;

public class AnimationTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1")) {
			var animator = this.gameObject.GetComponent<Animator>();
			animator.Play("WalkBlueChar");
		}	
		else{
			var animator = this.gameObject.GetComponent<Animator>();
			animator.Play("IdleBlueChar");
		}
	}
}
