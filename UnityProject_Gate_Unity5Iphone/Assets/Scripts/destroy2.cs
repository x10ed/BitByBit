using UnityEngine;
using System.Collections;

public class destroy2 : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		
		
		Destroy (other);
		Destroy (other.gameObject);
		destroyCount++;
	}
	
	
	void OnGUI () {
		GUI.Box(new Rect(10,210,100,30), "Destroy Count " + destroyCount.ToString());
	}
	int destroyCount = 0;
}
