using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

namespace Assets.Scripts.Common {
    public static class GameMaster {
		public static string levelName;
		public static string levelSelectName;
		public static List<Level> Levels;
		public static List<string>LevelList;
		public static string WorldList;
		public static Dictionary<string,string> WorldSelectSceneMapping = new Dictionary<string, string>();
		private static float lastAdvertTime;
		public static float AdvertisePause;

        public static MainMenu GetMainMenu() {
            GameObject level = GameObject.Find("_GameMaster");
            var mainMenuasObject = level.GetComponent<MainMenu>();
            if (mainMenuasObject != null) {
                var mainMenu = mainMenuasObject as MainMenu;
                if (mainMenu != null) return mainMenu;
            }
            return null;
        }
        public static ScoreBoard GetScoreBoard() {
            GameObject level = GameObject.Find("_GameMaster");
            var mainMenuasObject = level.GetComponent<ScoreBoard>();
            if (mainMenuasObject != null) {
                var mainMenu = mainMenuasObject as ScoreBoard;
                if (mainMenu != null) return mainMenu;
            }
            return null;
        }

        public static int GetScore() {
            return GetScoreBoard().GetScore();
        }

        public static int GetStarsCount() {
            return GetScoreBoard().GetScore();
        } 

		public static List<Level> GetMaps(){

			if (Levels != null && Levels.Count > 0)
				return Levels;

			LevelList = GetLevelList();
			Levels = new List<Level>();
			foreach (var levelNameVar in LevelList) {
                Levels.Add(MapMakker.GetMap(levelNameVar));
			}

			return Levels;
		}
		//kuna maaimade valikul tuleks see list uuesti genereerida
		//siis siin ei kontrolli, kas map list on täidetud
		public static List<Level> ReLoadMaps(){
						
			LevelList = GetLevelList();
			Levels = new List<Level>();
			foreach (var levelName in LevelList) {
				Levels.Add(MapMakker.GetMap(levelName));
			}
			
			return Levels;
		}

		public static Level GetCurrentLevel(){
			var result =  Levels == null ? null : Levels.FirstOrDefault (x => x.FileName == GameMaster.levelName);
			if (result != null) {
				return result;
			}
			// väike hack - vahepeal on vale maailma levelid laetud -- kui ei leia siis uuendame listi ja proovimu uuesti otsida
			ReLoadMaps();
			return Levels == null ? null : Levels.FirstOrDefault (x => x.FileName == GameMaster.levelName);

		}

        public static List<string> GetLevelList(string levelSelectNameParams = null) {
            levelSelectNameParams = levelSelectNameParams ?? GameMaster.levelSelectName;
            TextAsset txt = (TextAsset)Resources.Load("LevelData/" + levelSelectNameParams, typeof(TextAsset));
            string fileContents = txt.text;
		    var levelList = fileContents.Split (new string []{"\n",",", "\n","\r\n","\r"}, new System.StringSplitOptions()).ToList();
		    return levelList;
		}

		public static void LoadLevel(string levelName){
			Assets.Scripts.Common.GameMaster.levelName = levelName;
			string sceneName = FileReaderHelper.SceneName(levelName);
			Application.LoadLevel(sceneName);
		}

		public static void LoadLevelSelect(string levelSelectNameVar, string sceneName){
            Assets.Scripts.Common.GameMaster.levelSelectName = levelSelectNameVar;
			Application.LoadLevel(sceneName);
		}
		
		public static void LoadLevelSelect(string levelSelectNameVar){
			Assets.Scripts.Common.GameMaster.levelSelectName = levelSelectNameVar;
			if (!GameMaster.WorldSelectSceneMapping.ContainsKey(levelSelectNameVar)) { Application.LoadLevel("levelSelect"); return; }
			var sceneName = WorldSelectSceneMapping[levelSelectNameVar];
			Application.LoadLevel(sceneName);
		}

		// see FindObjectsOfTypeAll on liiga agar ja kipub meie prefabe ka muutma projektis
		public static void AdjustGUISizes() {
			float ratio = Screen.width / 1024.0f;
			if (ratio != 1f) {
				
				//			foreach (GUITexture b in Resources.FindObjectsOfTypeAll(typeof(GUITexture))) {
				//				Rect i = b.pixelInset;
				//				i.x *= ratio; i.y *= ratio; i.width *= ratio; i.height *= ratio;
				//				b.pixelInset = i;
				//			}
				//			
				foreach (GUIText tx in Resources.FindObjectsOfTypeAll(typeof(GUIText))) {
					if(!tx.gameObject.activeInHierarchy){
						continue;
					}
					
					float fontRatio = ratio; if (fontRatio < 0.85f) fontRatio = 0.85f;
					tx.fontSize = (int)(tx.fontSize * fontRatio);
					//tx.pixelOffset *= ratio;
				}
				
			}	
		}

		public static void EnableGates(){
			
			var disableds = Resources.FindObjectsOfTypeAll (typeof(Disableable)) as Disableable[];
			
			if(disableds != null){
				foreach (var disabled in disableds) {
					disabled.IsDisabled = false;
				}
			}
			
			var baseGates = Resources.FindObjectsOfTypeAll (typeof(BaseGate)) as BaseGate[];
			
			if(baseGates != null){
				foreach (var baseGate in baseGates) {
					baseGate.IsDisabled = false;
				}
			}		
			
			var animationHandlers = Resources.FindObjectsOfTypeAll (typeof(AnimationHandler)) as AnimationHandler[];
			
			if(animationHandlers != null){
				foreach (var animationHandler in animationHandlers) {
					if(!animationHandler.gameObject.activeInHierarchy){
						continue;
					}
					animationHandler.PlayCurrentAnimation();
				}
			}
		}

		public static void HideGateLables(bool hideLabel){
			//kaotame ära lable tekstid mis läbi win screen läbi kuvavad
			var templates = GameObject.FindGameObjectsWithTag ("Template");
			foreach (var template in templates) {
				var gate = template.GetComponent<BaseGate>();
				if(gate != null){
					gate.hiddeLabel = hideLabel;	
				}
			}
		}

		public static bool CanPlayAdd(){

			if(!MainMenu.GetShowAdds())
				return false;

			if(levelName == "Level1" || levelName == "Level25"){
				return false;
			}

			if (lastAdvertTime == 0) {
				lastAdvertTime = Time.time;
				return true;
			}

			if ( Time.time > (lastAdvertTime + AdvertisePause)) {
				lastAdvertTime = Time.time;
				return true;
			}

			return false;
		}
    }
}