using Assets.Scripts;
using Assets.Scripts.Common;
using UnityEngine;
using System.Collections;

public class InfiniteCloner : Disableable {

    private bool isClone;
    public string AfterClonTagName = "Gate";

	void OnMouseDown() {
		if (IsDisabledStartAnimation){return;}
		if (IsDisabled) { return; }

        if (!isClone) {
            ScoreBoard scoreBoard = GameMaster.GetScoreBoard();
            if (scoreBoard != null) {
                
            
            bool isCloneAllowed = scoreBoard.IsCloneAllowed(this.gameObject);
            if (!isCloneAllowed) {
                var draggable = this.gameObject.GetComponent<Draggable>();
                var baseGate = this.gameObject.GetComponent<BaseGate>();
                baseGate.IsDisabled = 
                draggable.IsDisabled = true; // ei luba liigutada viimast elementi
                return;
            }
            else {
                var draggable = this.gameObject.GetComponent<Draggable>();
                var baseGate = this.gameObject.GetComponent<BaseGate>();
                if (baseGate != null) {
                    baseGate.IsDisabled = false;
                }
                if (draggable != null) {
                    draggable.IsDisabled = false;
                }
            }
            }
            GameObject clone;
            isClone = true;
            clone = Instantiate(this.gameObject, this.gameObject.transform.position, transform.rotation) as GameObject;
            if (clone != null) {
                clone.transform.parent = this.transform.parent;
                if (clone.GetComponent<Renderer>() != null) {
                    var nameVar = clone.GetComponent<Renderer>().material.name;// uniti cloonimise kiiks. kui ei küsi siis ei clooni
                }
                clone.transform.localScale = transform.localScale;
                foreach (Transform child in clone.transform) {
                    if (child.GetComponent<Renderer>() != null) {
                        var childName = child.GetComponent<Renderer>().material.name;
                    }
                }
            }
            this.gameObject.tag = AfterClonTagName;
			var redDot = this.transform.Find("nr_BG");
			if(redDot != null){
				Destroy (redDot.gameObject);
			}
        }
    }
}
