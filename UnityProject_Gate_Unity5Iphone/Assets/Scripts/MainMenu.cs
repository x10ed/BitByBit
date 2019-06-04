using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using UnityEngine;
using System;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using AssemblyCSharp;
using Assets.Scripts;

namespace Assets.Scripts {
    public static class IpiCounter {
        private static int count;
        public static void  Reset() {
              var ipis = GameObject.FindGameObjectsWithTag ("Ball");
            count = ipis.Length;
        }

        public static void Minus() {
            count--;
        }

        public static bool Any() {
            return count > 0;
        }
    } 
    public class MainMenu : MonoBehaviour {

        public static bool go;

        private static int finishedIpis = 0;
        public GameObject WinScreen;
        public GUIStyle reset;
        public static bool redirectToShopScreen;
        public GUIStyle next;
        private static bool IsFreeWorld;
        public Level level;
        public static List<Vector3> GatesPosition;
		public static bool isLoosing;
		public static int animatingTileCount;
		public static bool startTutorialAnimation;
		public AudioClip winConditionAudio;
		public static LevelProgressListWrapper levelProgressWrapper;
		private static bool IsFreeVersion;
		private static bool IsSchoolVersion;
        public static bool IsAddFreeVar = false;
        private static bool ShowAdds;

        void Awake() {
//		originalCounter = finishLineCounter;
            var CMPlayer = GameObject.FindObjectOfType<ContinuesMusicPlayer>();
            if (CMPlayer != null) {
                CMPlayer.SetSound();
            }
        }

        void Start() {
            GatesPosition = new List<Vector3>();
            finishedIpis = 0;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            SetGO(false);
            Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
            //Tundub,et unity kiiks, et seda tuleb ise alati settida
            SetDeviceRotationOptions ();
            IpiCounter.Reset();
			animatingTileCount = 0;
			levelProgressWrapper = LevelProgressHelper.GetLevelProgress ();
        }

        public bool IsWinningCheckAlreadyHappened;
        void Update() {
            if (IpiCounter.Any()) {	
                return;
            }
            //Selleks, et surma animatsioon mängitaks lõpuni viimastel ipidel
            var ipis = GameObject.FindGameObjectsWithTag("Ball");
            if (ipis != null && ipis.Length > 0) {
                return;
            }
            if (IsWinning() && !IsWinningCheckAlreadyHappened) {
                var starCount = GetCurrentSessionStarCount();
                MarkLevelCompleted();
                UnLockNextLevel();        
                IsWinningCheckAlreadyHappened = true;

                var startBtn = GameObject.Find("startBtn");
                if (startBtn != null) {
                    var startOnClick =  startBtn.GetComponent<StartOnClick>();
                    if(startOnClick != null){
                        startOnClick.ResetStartTexture();
                    }
                }

                GameMaster.HideGateLables(true);

                GameObject scoreBG = GameObject.Find("scoreBG");
                if (scoreBG != null) {
                    if (scoreBG.GetComponent<Renderer>() != null) {
                        scoreBG.GetComponent<Renderer>().enabled = true;
                    }
                }
                if(WinScreen != null){

                    var winScreen = Instantiate (WinScreen, WinScreen.transform.position, Quaternion.identity) as GameObject;
					AudioSource.PlayClipAtPoint (winConditionAudio, transform.position);

                    foreach (Transform child in winScreen.transform) {		
                        if(child.name == "star1"  && starCount < 1){
                            DestoryActivStar(child);
                        }else if(child.name == "star2"  && starCount < 2){
                            DestoryActivStar(child);
                        }else if(child.name == "star3"  && starCount < 3){
                            DestoryActivStar(child);
                        }
                    }	
                }

            }else if(go && !IsWinningCheckAlreadyHappened){
                MainMenu.UpdateCurrentLevelIpisMaxCount();
				isLoosing = true;
            }
        }


        private void DestoryActivStar(Transform starObject){
            foreach(Transform child in starObject) {
                if(child.name == "star_active_bg"){
                    Destroy(child.gameObject);
                }
            }
        }

        private static void DisableBtn() {
            GameObject startBtn = GameObject.Find("startBtn");

            if (startBtn != null) {

                Destroy(startBtn);
            }
        }

        public void Reset() {
            var scoreBG = GameObject.Find("scoreBG");
            if (scoreBG != null) {
                scoreBG.GetComponent<Renderer>().enabled = false;
            }

            SetGO(false);
            var balls = GameObject.FindGameObjectsWithTag("Ball");

            foreach (var ball in balls) {
                Destroy(ball);
            }

            level = MapMakker.GetMap(GameMaster.levelName);
            IsWinningCheckAlreadyHappened = false;
            finishedIpis = 0;
            var resetebelList = FindAllByInterface<IReset>(); ;
            foreach (var gunsObject in resetebelList) {
                IReset resetebelGameObject = gunsObject as IReset;
                resetebelGameObject.Reset();

            }
            IpiCounter.Reset(); 
        }

        public void ResetGates(){
            var gates = GameObject.FindGameObjectsWithTag ("Gate");
            finishedIpis = 0;
            foreach (var gate in gates) {
                Destroy(gate);
            }

            GameMaster.GetScoreBoard().Clear();
        }

        public List<T> FindAllByInterface<T>() where T : class {
            List<T> list = new List<T>();
            var balls = GameObject.FindObjectsOfType(typeof(MonoBehaviour));

            foreach (MonoBehaviour o in balls) {
                T resetObject = o.transform.gameObject.GetComponent(typeof(T)) as T;
                if (resetObject != null) {
                    list.Add(resetObject);
                }
            }

            return list;
        }

        public void StopOnWin() {

            var balls = GameObject.FindGameObjectsWithTag("Ball");
            var gates = GameObject.FindGameObjectsWithTag("Gate");

            foreach (var ball in balls) {
                Destroy(ball);
            }

            foreach (var gate in gates) {
                Destroy(gate);
            }
            GameMaster.GetScoreBoard().Clear();
        }

        public void DestroyGates(){
            var gates = GameObject.FindObjectsOfType(typeof(BaseGate));

            foreach (BaseGate gate in gates) {
                Destroy(gate.gameObject);
            }
        }

        public static void SetGO(bool value) {
            go = value;
            var balls = GameObject.FindObjectsOfType(typeof(Ball));
            foreach (var ballObject in balls) {
                Ball ball = ballObject as Ball;
                ball.go = value;
            }
            var gunns = GameObject.FindObjectsOfType(typeof(Gun));
            foreach (var gunObject in gunns) {
                Gun gun = gunObject as Gun;
                gun.SetGo(value);
            }
        }

        public void SetMinusOne(){
            finishedIpis++;
        }

        public bool IsWinning() {
            if (level == null) {
                return false;		
            }
            int finishLineCounter =level.LevelWin- finishedIpis ;
            return finishLineCounter <= 0; ;

        }

        public static void StartStopToggle() {
            SetGO(!go);
        }

        public static void StartGame() {
            SetGO(true);
            IpiCounter.Reset();
        }

        public static void MarkLevelCompleted(){
			levelProgressWrapper = LevelProgressHelper.GetLevelProgress();

            if (string.IsNullOrEmpty(Assets.Scripts.Common.GameMaster.levelName)) {
                return;
            }

            var currentLevel = levelProgressWrapper.LevelProgress.FirstOrDefault (x => x.LevelName == GameMaster.levelName);

            if (currentLevel == null) {
                currentLevel = new LevelProgress(){LevelName = GameMaster.levelName};
                levelProgressWrapper.LevelProgress.Add(currentLevel);
            }
            currentLevel.IsCompleted = true;
            UpdateLevelIpisMaxCount(currentLevel);
            LevelProgressHelper.UpdateLevelProgress (levelProgressWrapper);
        }

		public static void UnLockNextLevel(){
            string nextName = LevelLoader.GetNextLevelNameButDoNotChangeStateOfCurrentGame(); // siin ei tohi setida Gamemasteri järgmise leveli nimeks 
            // AINULT UNLOCK -- kui kasutaja vajutab next nuppu viime järgmisele 
            levelProgressWrapper = LevelProgressHelper.GetLevelProgress();
		
            var currentLevel = levelProgressWrapper.LevelProgress.FirstOrDefault (x => x.LevelName == nextName);
		
            if (currentLevel == null) {
				currentLevel = new LevelProgress(){LevelName = nextName};
                levelProgressWrapper.LevelProgress.Add(currentLevel);
            }
            currentLevel.IsUnLocked = true;
            LevelProgressHelper.UpdateLevelProgress (levelProgressWrapper);
        }

        public static void UpdateLevelIpisMaxCount (LevelProgress currentLevel){
            if (currentLevel.FinishedCount == 0 || currentLevel.FinishedCount < finishedIpis) {
                currentLevel.FinishedCount = finishedIpis;
            }
        }

        public static int GetCurrentSessionStarCount(){
            var currentLevel = GameMaster.GetCurrentLevel ();
            if (currentLevel == null) {
                return 0;
            }
            return LevelProgressHelper.GetLevelStarCount (currentLevel, finishedIpis);
        }

        public static void UpdateCurrentLevelIpisMaxCount(){
            levelProgressWrapper = LevelProgressHelper.GetLevelProgress();
		
            if (string.IsNullOrEmpty(Assets.Scripts.Common.GameMaster.levelName)) {
                return;
            }
		
            var currentLevel = levelProgressWrapper.LevelProgress.FirstOrDefault (x => x.LevelName == Assets.Scripts.Common.GameMaster.levelName);
		
            if (currentLevel == null) {
                currentLevel = new LevelProgress(){LevelName = GameMaster.levelName};
                levelProgressWrapper.LevelProgress.Add(currentLevel);
            }
            UpdateLevelIpisMaxCount(currentLevel);
            LevelProgressHelper.UpdateLevelProgress (levelProgressWrapper);
        }



        private void SetDeviceRotationOptions (){
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
        }

		public static bool? IsFullVersionVar = null;

        public static bool IsFullVersion(){
			if (IsFreeVersion || IsSchoolVersion) { return true; }

			if (!IsFullVersionVar.GetValueOrDefault ()) {
				bool isFullVersion = false;
				bool.TryParse (PlayerPrefs.GetString ("FullVersion"), out isFullVersion);
				IsFullVersionVar = isFullVersion;
			}

			return IsFullVersionVar.GetValueOrDefault();
        }

		public static void SetIsSchoolVersion(){
			IsSchoolVersion = true;
		}

		public static void SetIsFreeVersion(){
			IsFreeVersion = true;
		}
		public static bool GetIsSchoolVersion(){
			return IsSchoolVersion;
		}
		public static bool GetIsFreeVersion(){
			return IsFreeVersion;
		}

		public static void SetShowAdds(){
			ShowAdds = true;
		}
		
		public static bool GetShowAdds(){
			return ShowAdds;
		}

		public static void SetIsFreeWorld(){
			IsFreeWorld = true;
		}
		
		public static bool GetIsFreeWorld(){
			return IsFreeWorld || IsSchoolVersion || IsFreeVersion;
		}
    }
}
