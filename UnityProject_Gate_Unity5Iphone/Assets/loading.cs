using UnityEngine;
using System.Collections;

public class loading : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.GetStreamProgressForLevel(1) == 1) {

		} else {
			float loading = Application.GetStreamProgressForLevel(1);
		}
	}
}
