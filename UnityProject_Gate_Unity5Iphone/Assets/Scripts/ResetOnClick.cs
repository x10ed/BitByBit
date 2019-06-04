using Assets.Scripts.Common;
using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class ResetOnClick : MonoBehaviour {
    void OnMouseUp() {
		//reset käitub nii nagu tavaline reset
		//ehk gated peavad alles jääma
		//LevelLoader.LoadLevelAgain ();
		GameMaster.GetMainMenu().Reset();
		GameMaster.EnableGates();
		GameMaster.HideGateLables(false);
		MainMenu.animatingTileCount = 0;
		Destroy(transform.parent.gameObject);
    }
}
