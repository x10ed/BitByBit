using UnityEngine;
using System.Collections;
using Assets.Scripts.Common;
using Assets.Scripts;

public class ResetPlayerPrefOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
		PlayerPrefs.DeleteKey("FullVersion");
		PlayerPrefs.DeleteKey("LevelProgress");
		if (MainMenu.GetIsSchoolVersion ()) {
			Application.LoadLevel ("worldSelectSchool");
		} else {
				Application.LoadLevel ("worldSelect");
		}
	}
}
