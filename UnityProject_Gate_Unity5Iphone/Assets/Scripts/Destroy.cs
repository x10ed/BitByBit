using Assets.Scripts.Common;
using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	public string ObjectToDestroyTagName = "Ball";

	void OnTriggerStay(Collider other) {

		if (other.gameObject.tag == ObjectToDestroyTagName && Input.GetMouseButtonUp(0)){
		    ScoreBoard scoreBoard = GameMaster.GetScoreBoard();
		    if (scoreBoard != null) scoreBoard.CountDestroy(other.gameObject);
		    Destroy (other.gameObject);
		}
	}
}
