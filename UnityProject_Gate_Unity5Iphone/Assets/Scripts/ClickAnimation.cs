using UnityEngine;
using System.Collections;

public class ClickAnimation : Disableable {

	private Vector3 OriginalSize;
	public float sizeMultiplier;
	public bool skipIsIsDisabledStartAnimation;

	void OnMouseDown(){
		if (IsDisabledStartAnimation && !skipIsIsDisabledStartAnimation){return;}
		if (IsDisabled && !skipIsIsDisabledStartAnimation) { return; }
		            
		if(tag != "Template"){
			OriginalSize = this.transform.localScale;
			this.transform.localScale = new Vector3 (transform.localScale.x * sizeMultiplier, transform.localScale.y * sizeMultiplier, transform.localScale.z);;
		}
	}

	void OnMouseUp(){
		if (IsDisabledStartAnimation){return;}
		if (IsDisabled) { return; }

		if (tag != "Template") {
			this.transform.localScale = OriginalSize;
		}
	}
}
