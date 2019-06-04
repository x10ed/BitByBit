using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class playButton : MonoBehaviour {

	void OnMouseDown(){
		MainMenu.StartStopToggle ();
	}
}
