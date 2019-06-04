using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class Level {
        public string FileName;
        public List<InventoryInfo> Inventory;
        public string InventoryBackground;
        public string InventoryStartXPosition = "-3";
        public string InventoryStartYPosition = "-5";
        public bool IsAllwaysUnlocked;
        public string LevelName;
        //public int BlueWinCondition;
        //public int OrangeWinCondition;
        public int LevelWin;
        public string NextLevel;
        public string NextWorld;
        public string OnStartPopUp;
        public int OneStar;
        public string SceneName;
        public string StartXPosition;
        public string StartYPosition;
        public int ThreeStars;
        public int TwoStars;
        public string[] UnlocksLevels;
        public List<Info> elements;

        public bool NextWorldIsSet {
            get { return !string.IsNullOrEmpty(NextWorld); }
        }
    }

    public class InventoryInfo : Info {
        public int count;

        public InventoryInfo(string name, int count) : base(name) {
            this.count = count;
        }
    }

    public class Info {
        protected const float X_Offset = 2;
        protected const float Y_Offset = -1.5f;
        public string prefabName;
        public int x;
        public int y;

        public Info(int x, int y, string name) {
            this.x = x;
            this.y = y;
            prefabName = name;
        }

        public Info(string name) {
            prefabName = name;
        }

        public virtual Vector3 GetPosition(float startX, float startY, float zPosition) {
            return new Vector3((x*X_Offset) + startX, (y*Y_Offset) + startY, zPosition);
        }

        public virtual Vector3 GetPosition(string startXStr, string startYStr, float zPosition) {
            float startX = 0;
            float startY = 0;
            float.TryParse(startXStr.Replace(',', '.'), out startX);
            float.TryParse(startYStr.Replace(',', '.'), out startY);
            return GetPosition(startX, startY, zPosition);
        }
    }

    public class MapMakker {
        public static Level GetMap(string levelName) {
            //var fileName = levelName + ".txt";
            //var sr = new StreamReader(Application.dataPath + "/Resources/LevelData/" + fileName);
            //var fileContents = sr.ReadToEnd();
            //sr.Close();
            var txt = (TextAsset) Resources.Load("LevelData/" + levelName, typeof (TextAsset));
            string fileContents = txt.text;
            string[] data = fileContents.Split("\n"[0]);
            return GetMap(data, levelName);
        }

        public static Level GetMap(string[] data, string fileName) {
            var level = new Level();
            var list = new List<Info>();
            level.Inventory = new List<InventoryInfo>();
            int rowCount = 0;
            foreach (string dataLine in data) {
                if (dataLine.Contains("Scene:")) {
                    level.SceneName = dataLine.Split(new[] {":", "\n", "\r\n", "\r"}, new StringSplitOptions())[1];
                    continue;
                }
                if (dataLine.Contains("Name:")) {
                    level.LevelName = dataLine.Split(new[] {":", "\n", "\r\n", "\r"}, new StringSplitOptions())[1];
                    continue;
                }
                if (dataLine.Contains("StartPositionX:")) {
                    level.StartXPosition = dataLine.Split(new[] {":", "\n", "\r\n", "\r"}, new StringSplitOptions())[1];
                    continue;
                }
                if (dataLine.Contains("StartPositionY:")) {
                    level.StartYPosition = dataLine.Split(new[] {":", "\n", "\r\n", "\r"}, new StringSplitOptions())[1];
                    continue;
                }
                if (dataLine.Contains("InventoryBackground:")) {
                    level.InventoryBackground = dataLine.Split(new[] {":", "\n", "\r\n", "\r"}, new StringSplitOptions())[1];
                    continue;
                }
                if (dataLine.Contains("OnStartPopUp:")) {
                    level.OnStartPopUp = dataLine.Split(new[] {":", "\n", "\r\n", "\r"}, new StringSplitOptions())[1];
                    continue;
                }
                if (dataLine.Contains("IsAllwaysUnlocked:")) {
                    bool isAllwaysUnlocked = false;
                    bool.TryParse(dataLine.Split(new[] {":", "\n", "\r\n", "\r"}, new StringSplitOptions())[1],
                        out isAllwaysUnlocked);
                    level.IsAllwaysUnlocked = isAllwaysUnlocked;
                    continue;
                }
                if (dataLine.Contains("UnlocksLevels:")) {
                    level.UnlocksLevels =
                        dataLine.Split(new[] {"UnlocksLevels:", "\n", "\r\n", "\r"}, new StringSplitOptions())[1].Split(';');
                    continue;
                }
                if (dataLine.Contains("OneStar:")) {
                    int val = 0;
                    int.TryParse(dataLine.Split(new[] {"OneStar:", "\n", "\r\n", "\r"}, new StringSplitOptions())[1],
                        out val);
                    level.OneStar = val;
                    continue;
                }
                if (dataLine.Contains("TwoStars:")) {
                    int val = 0;
                    int.TryParse(dataLine.Split(new[] {"TwoStars:", "\n", "\r\n", "\r"}, new StringSplitOptions())[1],
                        out val);
                    level.TwoStars = val;
                    continue;
                }
                if (dataLine.Contains("ThreeStars:")) {
                    int val = 0;
                    int.TryParse(dataLine.Split(new[] {"ThreeStars:", "\n", "\r\n", "\r"}, new StringSplitOptions())[1],
                        out val);
                    level.ThreeStars = val;
                    continue;
                }
                if (dataLine.Contains("LevelWin:")) {
                    int val = 0;
                    int.TryParse(dataLine.Split(new[] {"LevelWin:", "\n", "\r\n", "\r"}, new StringSplitOptions())[1],
                        out val);
                    level.LevelWin = val;
                    continue;
                }
                if (dataLine.Contains("Nextworld:")) {
                    string nextWorldData =
                        dataLine.Split(new[] {"Nextworld:", "\n", "\r\n", "\r"}, new StringSplitOptions())[1];
                    string[] keypars = nextWorldData.Split(',');
                    level.NextWorld = keypars[0];
                    level.NextLevel = keypars[1];
					level.UnlocksLevels = new string[1] { keypars[1]};
                    continue;
                }

                if (dataLine.Contains("Inventory:")) {
                    string prefabNames =
                        dataLine.Split(new[] {"Inventory:", "\n", "\r\n", "\r"}, new StringSplitOptions())[1];
                    string[] keypars = prefabNames.Split(',');

                    int inventoryCellCount = 0;
                    foreach (string pair in keypars) {
                        string[] splitArray = pair.Split(';');
                        string prefabName = splitArray[0];
                        int count = int.Parse(splitArray[1]);

                        level.Inventory.Add(new InventoryInfo(prefabName, count));
                        inventoryCellCount++;
                    }
                    continue;
                }
                int cellCount = 0;
                foreach (string dataCell in dataLine.Split(new[] {",", "\n", "\r\n", "\r"}, new StringSplitOptions())) {
                    if (!string.IsNullOrEmpty(dataCell))
                        list.Add(new Info(++cellCount, rowCount, dataCell));
                }
                rowCount++;
            }
            level.elements = list;
            level.FileName = fileName;
            return level;
        }
    }
}