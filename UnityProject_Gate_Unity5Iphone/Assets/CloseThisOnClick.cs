using UnityEngine;
using System.Collections;
using Assets.Scripts.Common;
using Assets.Scripts;

public class CloseThisOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Assets.Scripts.Common.GameMaster.HideGateLables(true);
		MainMenu.startTutorialAnimation = false;
	}
	bool firstTime = true;
	// Update is called once per frame
	void Update () {
		if (firstTime) {
				Assets.Scripts.Common.GameMaster.HideGateLables (true);
				firstTime = false;
		}
	}
	void OnMouseUp() {
		Assets.Scripts.Common.GameMaster.HideGateLables(false);
		if(this.gameObject.GetComponent<slide>() != null){			
			MainMenu.animatingTileCount--;
		}
		MainMenu.startTutorialAnimation = true;
		Destroy (this.transform.parent.gameObject);
	}
}
