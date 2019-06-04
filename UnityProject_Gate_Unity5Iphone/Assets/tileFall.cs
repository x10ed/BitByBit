using UnityEngine;
using System.Collections;
using Assets.Scripts.Common;
using Assets.Scripts;

public class tileFall : MonoBehaviour {

	Vector3 finalPos;
	float lerpSpeed;
	bool animationEnded;
	float randomPick;

	void Start() {
		lerpSpeed = Random.Range (0.1F, 0.14F);
		MainMenu.animatingTileCount++;

		if(Random.value < 0.5f)
			randomPick = Random.Range(20.0f,15.0f);
		else
			randomPick = Random.Range(-20.0f,-15.0f) ;
		
		finalPos = this.transform.position;
		this.transform.position = new Vector3(this.transform.position.x - randomPick, randomPick, this.transform.position.z);
	}
	void Update(){

		if (animationEnded) {
			return;
		}

		if (IsSamePosition(this.transform.position,finalPos)) {
			this.transform.position = finalPos;
			animationEnded = true;
			MainMenu.animatingTileCount--;
			return;
		}

		this.transform.position = Vector3.Lerp (this.transform.position, finalPos, lerpSpeed);

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
}
