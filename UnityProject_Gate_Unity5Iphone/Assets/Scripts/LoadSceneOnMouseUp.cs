using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class LoadSceneOnMouseUp : MonoBehaviour {
	public string LevelName;
	public bool DisableAfterPurchase;

	void OnMouseUp(){
		if(DisableAfterPurchase && MainMenu.IsFullVersion()){
			return;
		}
		//we use this to rename this scene to school version
		if (LevelName.ToLower() == "worldselect" && (MainMenu.GetIsSchoolVersion() || MainMenu.GetIsFreeVersion())) 
		{LevelName = "worldSelectSchool";	}

		Application.LoadLevel(LevelName);
	}
}
