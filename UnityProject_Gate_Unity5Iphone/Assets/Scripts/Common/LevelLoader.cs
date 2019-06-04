using Assets.Scripts;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using System.Linq;

public class LevelLoader : MonoBehaviour {

	bool showSubMenu = false;
	//public int with = 0;

	// võtan ära pbublic propertyd et neid ei saaks üle kirjutada 
	int buttonWith = Screen.width / 10;
	 int dropDownButtonWith = 210;
	int buttonHeight = Screen.width / 10;
	 int dropDownButtonHeight = 75;
	//int _buttonSpace;
     int _dropDownButtonSpace = 75;

	public GUIStyle levelSelect;
	public GUIStyle levelNameBG;

	private static List<string> LevelNames = new List<string> {"startScreen","gate_v11","gate_v8","gate_v12","gate_v13","gate_v6","gate_v7","gate_v9","gate_v10"};
	// Use this for initialization

	void Start () {
	}

	public static void LoadNextLevel(){
		string nextName= GetNextLevelName ();

		if (MainMenu.redirectToShopScreen) {
			Application.LoadLevel("shopScreen");
			return;
		}
		if (nextName != null) {
			GameMaster.LoadLevel(nextName);
		}
	
	}

	public static string GetNextLevelName(){

		var levelList = GameMaster.LevelList;

		if (levelList == null || !levelList.Any ()) {
			GameMaster.LevelList = GameMaster.GetLevelList();
			levelList = GameMaster.LevelList;
		}
		int index  = levelList.IndexOf (GameMaster.levelName);
		int nextIndex = index + 1;

		//if last level, switch new world
		if (levelList.Count == nextIndex) {
			//if Free version, then we direct player to buy screen
			SetNextWorldFirstLevel();
			if(!MainMenu.IsFullVersion()){
				MainMenu.redirectToShopScreen = true;
				return GameMaster.levelName;
			}
			return GameMaster.levelName;
		}

		return levelList[nextIndex];
	}

    public static string GetNextLevelNameButDoNotChangeStateOfCurrentGame() {
        // ei tohi settida current levelit mingiks muuks - tagastab ainult järgmise nime - kordan EI seti midagi !!!!!!!!!!!1 türi paide
        var levelList = GameMaster.LevelList;

        if (levelList == null || !levelList.Any()) {
            GameMaster.LevelList = GameMaster.GetLevelList();
            levelList = GameMaster.LevelList;
        }
        int index = levelList.IndexOf(GameMaster.levelName);
        int nextIndex = index + 1;

        //if last level, switch new world
        if (levelList.Count == nextIndex) {
            //if Free version, then we direct player to buy screen

            var level = GameMaster.Levels.FirstOrDefault(x => x.FileName == GameMaster.levelName);


            levelList = GameMaster.GetLevelList(level.NextWorld);
            
            if (!MainMenu.IsFullVersion()) {
                MainMenu.redirectToShopScreen = true;
            }
            return level.NextLevel;
        }

        return levelList[nextIndex];
    }

	public static void SetNextWorldFirstLevel(){
		var level = GameMaster.Levels.FirstOrDefault (x => x.FileName == GameMaster.levelName);

		if (level.NextWorldIsSet) {
			GameMaster.levelSelectName = level.NextWorld;
			GameMaster.levelName = level.NextLevel;
			GameMaster.LevelList = GameMaster.GetLevelList();
		}
	}

	public static string GetCurrentSceneName(){
		//return UnityEditor.EditorApplication.currentScene;
		return Application.loadedLevelName;
	}

    void OnGUI() {

        var buttoFinalWhitFromWindow = (Screen.width - (2 * buttonWith));
        var dropDownButtoFinalWhitFromWindow = (Screen.width - (2 * dropDownButtonWith));

        if (!showSubMenu) {
            if (GUI.Button(new Rect(buttoFinalWhitFromWindow, 0, buttonWith, buttonHeight), "", levelSelect)) {
                showSubMenu = true;
            }

        }
        else {

            int elementNumber = 1;
            for (int i = 0; i < LevelNames.Count; i++) {
                string item = LevelNames[i];
                if (GUI.Button(new Rect(dropDownButtoFinalWhitFromWindow, _dropDownButtonSpace * elementNumber, dropDownButtonWith, dropDownButtonHeight), item, levelNameBG)) {
                    Application.LoadLevel(item);
                    showSubMenu = false;
                }
                elementNumber++;
            }

            if (GUI.Button(new Rect(dropDownButtoFinalWhitFromWindow, _dropDownButtonSpace * elementNumber, dropDownButtonWith, dropDownButtonHeight), "Cancel", levelNameBG)) {

                showSubMenu = false;
            }

            elementNumber++;
            if (GUI.Button(new Rect(dropDownButtoFinalWhitFromWindow, _dropDownButtonSpace * elementNumber, dropDownButtonWith, dropDownButtonHeight), "Next", levelNameBG)) {
                LoadNextLevel();
                showSubMenu = false;
            }
        }
    }

	public static void LoadLevelSelect(){
		Application.LoadLevel("levelSelect");
	}

    public static void LoadLevelAgain() {
		GameMaster.LoadLevel (GameMaster.levelName);
    }	
}
