using UnityEngine;
using System.Collections;

public class fadeIn : MonoBehaviour {

	public float speed;
	public float wait;
	bool animationEnded;
	bool started;
	Color color;
	float maxAlpha;


	void Start () {
		// At start, use the first material
		color = GetComponent<Renderer>().material.color;
		maxAlpha = GetComponent<Renderer>().material.color.a;
		color.a = 0;
		GetComponent<Renderer>().material.color = color;

	}
	
	void FixedUpdate () {

		if ((GetComponent<Renderer>().enabled == false) && (started == false)) {
			color = GetComponent<Renderer>().material.color;
			color.a = 0;
			GetComponent<Renderer>().material.color = color;
			started = true;
		}
		if (animationEnded) {
			return;
		}

		if (GetComponent<Renderer>().enabled == true){
			StartCoroutine ("Fade");
		}

	}

	IEnumerator Fade(){
		yield return new WaitForSeconds(wait);

		if (color.a < maxAlpha) {
			color.a += speed * Time.deltaTime;
			GetComponent<Renderer>().material.color = color;
		} else {
			animationEnded = true;
		}
	}
}
