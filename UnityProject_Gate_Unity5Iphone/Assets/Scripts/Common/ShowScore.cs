using Assets.Scripts;
using Assets.Scripts.Common;
using UnityEngine;
using System.Collections;

public class ShowScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    var score = GameMaster.GetScoreBoard().GetScore();
        this.SetLabelValue(score);
	}
}
