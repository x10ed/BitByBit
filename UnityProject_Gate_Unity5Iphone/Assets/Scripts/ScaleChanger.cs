using UnityEngine;
using System.Collections;

public class ScaleChanger : MonoBehaviour {

	public float sizeMultiplier;
	private bool isChanged = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void  OnMouseDown(){
		if (transform.tag != "Template" && !isChanged) {
			transform.localScale = new Vector3 (transform.localScale.x * sizeMultiplier, transform.localScale.y * sizeMultiplier, transform.localScale.z);
			isChanged = true;
		}
	}
}
