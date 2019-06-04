using UnityEngine;
using System.Collections;

public class OpenWebUrlOnClick : MonoBehaviour {

	GameObject parental;

	void Start(){
		parental = GameObject.Find("parentalGate");
		parental.SetActive(false);
	}

	 void OnMouseUp(){
		parental.SetActive(true);
	} 
}
