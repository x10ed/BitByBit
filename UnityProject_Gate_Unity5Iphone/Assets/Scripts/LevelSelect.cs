using UnityEngine;
using System.Collections;
using Assets.Scripts.Common;

public class LevelSelect : MonoBehaviour {

	public string LevelName;
	public string LevelPath;
	public bool IsUnLocked;
	
	void OnMouseUp(){
		GameMaster.LoadLevel (LevelName);
	}
}
