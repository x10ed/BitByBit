using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Common
{
	public static class FileReaderHelper
	{
        
		public static Level GetMapInfo(string levelName){

			return GetMap(GetLevelContent(levelName));
		}

		public static string SceneName(string levelName) {
		    var data = GetLevelContent(levelName);
		    var firstOrDefault = data.FirstOrDefault(x => x.Contains("Scene:"));
		    if (firstOrDefault != null)
		        return firstOrDefault.Split(new string[] { ":", "\n", "\r\n","\r" }, new System.StringSplitOptions())[1];
            throw new ApplicationException("data.FirstOrDefault(x => x.Contains(Scene:)); - is NULL ");
		}

	    public static string[] GetLevelContent(string levelName){
			//var sr = new StreamReader(filePath);
			//var fileContents = sr.ReadToEnd();
			//sr.Close();
            TextAsset txt = (TextAsset)Resources.Load("LevelData/"+ levelName, typeof(TextAsset));
            string fileContents = txt.text;
			return fileContents.Split("\n"[0]);
		}

		public static Level GetMap(string[] data)
		{
			string sceneName = "";
			bool isAllwaysUnlocked = false;
			List<Info> list = new List<Info>();	
			int rowCount = 0;
			foreach (var dataLine in data) {
				if(dataLine.Contains("Scene:")){
					sceneName = dataLine.Split(new string []{":",  "\n", "\r\n","\r"},new System.StringSplitOptions())[1];
					continue;
				}
				if(dataLine.Contains("IsAllwaysUnlocked:")){
					bool.TryParse(dataLine.Split(new string[] { ":", "\n", "\r\n","\r" }, new System.StringSplitOptions())[1], out isAllwaysUnlocked);
					continue;
				}
				int cellCount = 0;
				foreach (var dataCell in dataLine.Split(new string[] { ",", "\n", "\r\n","\r" }, new System.StringSplitOptions())) {
					if(!string.IsNullOrEmpty(dataCell))
						list.Add(new Info(++cellCount,rowCount,dataCell));			
				}
				rowCount++;
			}

			Level level = new Level ();
			level.elements = list;
			level.SceneName = sceneName;
			level.IsAllwaysUnlocked = isAllwaysUnlocked;

			return level;
		}
	}
}

