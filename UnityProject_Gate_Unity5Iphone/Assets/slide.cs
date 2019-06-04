using UnityEngine;
using System.Collections;
using Assets.Scripts.Common;
using Assets.Scripts;

public class slide : MonoBehaviour {

	Vector3 finalPos;
	public float lerpSpeed;
	public bool inheritPosX;
	public float startPosX;
	public bool inheritPosY;
	public float startPosY;

	public float wait;
	bool animationEnded;

	void Start() {
		MainMenu.animatingTileCount++;
		if (inheritPosX == true) {
			startPosX = this.transform.position.x;		
		}
		if (inheritPosY == true) {
			startPosY = this.transform.position.y;		
		}

		finalPos = this.transform.position;
		this.transform.position = new Vector3(startPosX, startPosY, this.transform.position.z);

	}

	void FixedUpdate(){
		if (animationEnded) {
			return;
		}
		
		StartCoroutine("Slide");
	}

	bool IsSamePosition(Vector3 position1,Vector3 position2){
		
		bool isSame = true;
		if (Mathf.Abs(Mathf.Round(position1.x * 10f) - Mathf.Round (position2.x * 10f)) >= 1) {
			isSame = false;
		}
		
		if (Mathf.Abs(Mathf.Round (position1.y * 10f) - Mathf.Round (position2.y * 10f)) >= 1) {
			isSame = false;
		}
		return isSame;
	}

	IEnumerator Slide(){
		yield return new WaitForSeconds(wait);

		if (!IsSamePosition(this.transform.position, finalPos)) {
			this.transform.position = Vector3.Lerp (this.transform.position, finalPos, lerpSpeed * Time.deltaTime);
		} else {
			animationEnded = true;
			MainMenu.animatingTileCount--;
			this.transform.position = finalPos;
			Destroy(this.gameObject.GetComponent("slide"));
		}
	}
}
