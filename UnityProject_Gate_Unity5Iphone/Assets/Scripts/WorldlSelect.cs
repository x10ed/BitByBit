using Assets.Scripts;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Common;

public class WorldlSelect : MonoBehaviour {

	public string LevelSelectName;
	public string LevelSelectScene;
	public bool IsFree;

	void Start(){
		if(!GameMaster.WorldSelectSceneMapping.ContainsKey(LevelSelectName)){
			GameMaster.WorldSelectSceneMapping.Add (LevelSelectName, LevelSelectScene);
		}
	}

	void OnMouseUp(){
		if (IsFree) { MainMenu.SetIsFreeWorld (); }

		if (!string.IsNullOrEmpty (LevelSelectName)) {
			Assets.Scripts.Common.GameMaster.levelSelectName = LevelSelectName;
			GameMaster.LoadLevelSelect (LevelSelectName,LevelSelectScene);
		}
	}
}
