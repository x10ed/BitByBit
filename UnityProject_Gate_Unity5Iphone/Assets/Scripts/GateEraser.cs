using Assets.Scripts;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Common;

public class GateEraser : MonoBehaviour {

	void OnMouseDown(){
		if (!MainMenu.go) {
			GameMaster.GetMainMenu ().ResetGates ();
		}
	}
}
