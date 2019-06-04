using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts {
    public class MapGen : MonoBehaviour {
        private void Start() {
            Level level = MapMakker.GetMap(GameMaster.levelName);
            List<Info> list = level.elements;

            foreach (Info item in list) {
                var res = Resources.Load("Prefabs/tiles/" + item.prefabName) as GameObject;
                if (res == null) {
                    continue;
                }
                Instantiate(res, item.GetPosition(level.StartXPosition, level.StartYPosition, res.transform.position.z),
                    Quaternion.identity);
            }


            ScoreBoard scoreBoard = GameMaster.GetScoreBoard();
            MainMenu mainMenu = GameMaster.GetMainMenu();

            mainMenu.level = level;
            for (int index = 0; index < level.Inventory.Count; index++) {
                InventoryInfo item = level.Inventory[index];
                // add to scorebaord 
                scoreBoard.List.Add(new GateLimitWorkSet {number = item.count, name = item.prefabName});


                var res = Resources.Load("Prefabs/inventory/" + item.prefabName) as GameObject;
                if (res == null) {
                    Debug.Log("Resources.Load(Prefabs/inventory/" + item.prefabName + ") is null");
                    continue;
                }
                var v3 = new Vector3(-4.8f + (index*2.3f), -5.5f, 0);
                Instantiate(res, v3, Quaternion.identity);

                var background = Resources.Load("Prefabs/InventoryBackground") as GameObject;

                if (background == null) {
                    continue;
                }
                var vback = new Vector3(v3.x, -5.5f, 0.2f);
                Instantiate(
                    background, vback, Quaternion.identity);
            }
            GameObject popup = null;
            if (!string.IsNullOrEmpty(level.OnStartPopUp)) {
                popup = Resources.Load("Prefabs/tutorial/" + level.OnStartPopUp) as GameObject;
                if (popup == null) {
                    Debug.Log("Resources.Load(Prefabs/inventory/" + level.OnStartPopUp + ") is null");
                }
                else {
                    Instantiate(popup);
                }
            }


            scoreBoard.Clear();
            GameMaster.AdjustGUISizes();
            IpiCounter.Reset();
            ScoreBoard.UpdateLabel();
        }
    }
}