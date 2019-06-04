using UnityEngine;
using System.Collections;

public class StartActivityIndicator : MonoBehaviour {

	static bool loading_on ;
	public Texture2D Loading_Screen;
	
	void OnGUI () {
        //GUI.Box(new Rect(0, 0, 1024, 768), "test");
		if(loading_on){

			GUI.Box(new Rect(0,0,1024,768),"test");
		}
		if(!Application.isLoadingLevel)
			loading_on=false;
	}

}
