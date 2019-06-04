using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Assets.Scripts.Common;

public class LevelSelectGrid : MonoBehaviour {

	public float RowCount;
	public float XStart;
	public float YStart;
	public float XMargin;
	public float YMargin;
	public GameObject PrefabSelectable;
	public GameObject PrefabLoced;
	public GameObject PrefabBuy;
	public GUIStyle next;
	public GUIStyle back;
	public bool AllwaysUnLocked;

	// Use this for initialization
	void Start () {

		if (MainMenu.levelProgressWrapper == null) {
			MainMenu.levelProgressWrapper = LevelProgressHelper.GetLevelProgress ();
		}
		MainMenu.redirectToShopScreen = false;
		var levelList = GameMaster.ReLoadMaps();

		int totalCount = levelList.Count - 1;
		int cells = (int)Mathf.CeilToInt((levelList.Count / RowCount));
		int totalCounter = 0;		
		var position = new Vector3 (XStart, YStart);
		//Kuna meil on Y telg sassis siis arvutame seda teist pidi ehk peegelpildis
		var positionText = new Vector3 (XStart, YStart - YMargin);
		for (int s = 0; s < cells; s++) {
			for (int i = 0; i < RowCount; i++) {
				if(totalCounter > totalCount){
					break;
				}
				var levelMap = levelList[totalCounter];
				var levelProgress = MainMenu.levelProgressWrapper.LevelProgress.FirstOrDefault(x => x.LevelName == levelMap.FileName);	
				if (levelProgress == null) {
					levelProgress = new LevelProgress { LevelName = levelMap.FileName,IsUnLocked = AllwaysUnLocked };
					MainMenu.levelProgressWrapper.LevelProgress.Add(levelProgress);
				}else if (AllwaysUnLocked) {
					levelProgress.IsUnLocked = AllwaysUnLocked;
				}
				GameObject clone =  GetGridElement(levelMap,levelProgress,levelList,MainMenu.levelProgressWrapper,position);
				var starCount = LevelProgressHelper.GetLevelStarCount(levelProgress,levelMap);
				foreach (Transform child in clone.transform) {		
					LevelSelect levelSelectButton = child.GetComponent(typeof(LevelSelect)) as LevelSelect;
					if(levelSelectButton != null){						
						levelSelectButton.LevelName = levelMap.FileName;
						levelSelectButton.LevelPath = Application.dataPath + "/Resources/LevelData/" + levelMap.FileName + ".txt";
						levelSelectButton.IsUnLocked = levelProgress.IsCompleted;
					}
					ObjectLabel objectLabel = child.GetComponent(typeof(ObjectLabel)) as ObjectLabel;
					if(objectLabel){
						GUIText gUiText = objectLabel.GetComponent<GUIText>();
						if (gUiText != null) gUiText.text = (totalCounter + 1).ToString();
					}
					if(child.name == "stars_select" && starCount > 0){
						starCount--;
						child.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0,0);
					}
				}
				position.x += XMargin;
				positionText.x += XMargin;
				totalCounter ++;
			}
			position.x = XStart;
			position.y -= YMargin;
			positionText.y += YMargin;
			positionText.x = XStart;
		}
		GameMaster.AdjustGUISizes();
		LevelProgressHelper.UpdateLevelProgress(MainMenu.levelProgressWrapper);
	}

	public GameObject GetGridElement(Level levelMap,LevelProgress levelProgress, List<Level> levelMaps,LevelProgressListWrapper levelProgresses, Vector3 position){
		if(levelMap.IsAllwaysUnlocked || MainMenu.GetIsSchoolVersion()){
			return Instantiate (PrefabSelectable, position, PrefabSelectable.transform.rotation) as GameObject;
		}
		if (!MainMenu.GetIsFreeWorld() && !MainMenu.IsFullVersion ()) {
			return Instantiate (PrefabBuy, position, PrefabSelectable.transform.rotation) as GameObject;
		}
		if (levelProgress.IsCompleted) {
			return Instantiate (PrefabSelectable, position, PrefabSelectable.transform.rotation) as GameObject;
		}
		if (levelProgress.IsUnLocked) {
			return Instantiate (PrefabSelectable, position, PrefabSelectable.transform.rotation) as GameObject;
		}
		var levelsThatAreCompleted = levelProgresses.LevelProgress.Where (x => x.IsCompleted);

		if (levelsThatAreCompleted != null && levelsThatAreCompleted.Any()) {
			if(levelMaps.Any(x => x.UnlocksLevels != null && x.UnlocksLevels.Any(s => s == levelProgress.LevelName) && levelsThatAreCompleted.Any(v => v.LevelName == x.FileName))){
				return Instantiate (PrefabSelectable, position, PrefabSelectable.transform.rotation) as GameObject;
			}
		}

		return Instantiate (PrefabLoced, position, PrefabSelectable.transform.rotation) as GameObject;
	}
}

public static class LevelProgressHelper{
		
	public static LevelProgressListWrapper GetLevelProgress(){
		var value = PlayerPrefs.GetString("LevelProgress");
		
		if(string.IsNullOrEmpty(value))
			return new LevelProgressListWrapper();
		
		return  DeserializeObject <LevelProgressListWrapper>(value);
	}
	
	public static void UpdateLevelProgress(LevelProgressListWrapper levelProgress){
		string serialized = SerializeObject (levelProgress);
		PlayerPrefs.SetString("LevelProgress",serialized);
	}

	public static int GetLevelStarCount(LevelProgress levelProgress,Level levelMap){
		if (levelProgress.IsCompleted) {
			return GetLevelStarCount (levelMap, levelProgress.FinishedCount);
		}
		return 0;
	}

	
	public static int GetLevelStarCount(Level levelMap,int count){
		if(count >= levelMap.ThreeStars){
			return 3;
		}else if(count >= levelMap.TwoStars){
			return 2;
		}else if(count >= levelMap.OneStar){
			return 1;
		}

		return 0;
	}

	public static string SerializeObject<T>( T toSerialize)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
		StringWriter textWriter = new StringWriter();
		
		xmlSerializer.Serialize(textWriter, toSerialize);
		return textWriter.ToString();
	}	
	
	public static T DeserializeObject<T>( string toDeserialize)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		return (T)xmlSerializer.Deserialize (new StringReader(toDeserialize));
	}
}

[System.Serializable]
public class LevelProgress  {
	public string LevelName;
	public bool IsCompleted;
	public bool IsUnLocked;
	public int FinishedCount;	

	public override string ToString ()
	{
		return string.Format ("LevelName: " + LevelName+", IsCompleted: "+IsCompleted +", IsUnLocked: " + IsUnLocked + ", FinishedCount:" +FinishedCount);
	}
}

[System.Serializable]
public class LevelProgressListWrapper{
	public List<LevelProgress> LevelProgress;
	public LevelProgressListWrapper(){
		LevelProgress = new List<LevelProgress> ();
	}

}