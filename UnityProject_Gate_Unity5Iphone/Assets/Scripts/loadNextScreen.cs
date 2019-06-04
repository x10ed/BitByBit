using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts.Common;

public class loadNextScreen : MonoBehaviour {

	private string TextBoxText = "Username";
	private Rect TXTPosition = new Rect(Screen.width / 2 - 135,Screen.height/2,270,50);
	public GUIStyle Style;
	
	void OnGUI(){
		TextBoxText = GUI.TextField(TXTPosition,TextBoxText.Replace("\n", "").Replace("\r", ""),10,Style);
	}

	void Start(){
		Style.fontSize = Screen.width / 29;
	}

	void Update(){
		TextBoxText = TextBoxText.Replace ("\n", "").Replace("\r", "");
	}

	void OnMouseDown(){
		LevelLoader.LoadLevelSelect();

	}
}
