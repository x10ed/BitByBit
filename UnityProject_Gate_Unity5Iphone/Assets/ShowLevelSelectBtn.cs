using UnityEngine;
using System.Collections;
using Assets.Scripts.Common;

public class ShowLevelSelectBtn : MonoBehaviour {
	private string levelSelectName;

	// Use this for initialization
	void Start () {
		levelSelectName = GameMaster.levelSelectName;
	}

	void OnMouseUp() {
		GameMaster.levelSelectName = levelSelectName;
		GameMaster.LoadLevelSelect(levelSelectName);
	}
}
