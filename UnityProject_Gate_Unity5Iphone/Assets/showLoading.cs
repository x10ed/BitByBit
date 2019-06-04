using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Common;

public class showLoading : MonoBehaviour {



	void OnMouseDown(){
		//loadingIcon.renderer.enabled = true;

		var loadScene = this.gameObject.GetComponent<LoadSceneOnMouseUp> ();
		if (loadScene != null && loadScene.DisableAfterPurchase && MainMenu.IsFullVersion()) {
			return;
		}

		GameObject instance = Instantiate(Resources.Load("prefabs/loading_icon", typeof(GameObject))) as GameObject;
	}
}
