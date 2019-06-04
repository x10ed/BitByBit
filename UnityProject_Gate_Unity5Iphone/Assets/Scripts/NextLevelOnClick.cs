using Assets.Scripts;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Common;

public class NextLevelOnClick : MonoBehaviour {

	void OnMouseUp() {
		MainMenu.animatingTileCount = 0;
		LevelLoader.LoadNextLevel ();
	}
}
