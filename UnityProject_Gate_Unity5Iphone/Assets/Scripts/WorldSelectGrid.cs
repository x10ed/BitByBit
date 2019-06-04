using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Assets.Scripts.Common;

public class WorldSelectGrid : MonoBehaviour {

	public float RowCount;
	public float XStart;
	public float YStart;
	public float XMargin;
	public float YMargin;
	public string WorldList;

	void Start () {
		var worldList = GetWorldList().Where(x => !string.IsNullOrEmpty(x)).ToArray();
		int totalCount = worldList.Count() - 1;
		int cells = (int)Mathf.CeilToInt((worldList.Count() / RowCount));
		int totalCounter = 0;		
		var position = new Vector3 (XStart, YStart);
		//Kuna meil on Y telg sassis siis arvutame seda teist pidi ehk peegelpildis
		var positionText = new Vector3 (XStart, YStart - YMargin);

		for (int s = 0; s < cells; s++) {
			for (int i = 0; i < RowCount; i++) {
				if (totalCounter > totalCount) {
						break;
				}
				var worldName = worldList[totalCounter];
				var res = Resources.Load("Prefabs/world/" + worldName) as GameObject;
				if (res == null) {
					continue;
				}
				Instantiate(res, new Vector3(position.x, position.y, res.transform.position.z), Quaternion.identity);

				position.x += XMargin;
				positionText.x += XMargin;
				totalCounter ++;
			}
			position.x = XStart;
			position.y -= YMargin;
			positionText.y += YMargin;
			positionText.x = XStart;
		}
	}


	void Update () {
	
	}

	public string[] GetWorldList(){
		TextAsset txt = (TextAsset)Resources.Load("LevelData/" + WorldList, typeof(TextAsset));
		string fileContents = txt.text;
		return fileContents.Split(new string[] { "\n", ",","\r\n","\r" }, new System.StringSplitOptions());
	}
}
