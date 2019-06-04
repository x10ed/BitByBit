using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts;
using Assets.Scripts.Common;

public class AnimationHandler : MonoBehaviour {

	public GameObject SlideFingerAnimationPrefab;
	public GameObject ClickFingerAnimationPrefab;
	public GameObject ClickPlayAnimationPrefab;
	private GameObject slideFingerInstantce;
	private bool clickGateAnimation;
	private bool clickPlayAnimation;
	private bool slideFingerAnimation;
	private bool animationIsStarted;

	// Use this for initialization
	void Start () {
		if (GameMaster.levelName == "Level1") {
			PlayAnimation (SlideFingerAnimationPrefab);
		}
	}

	// Use this for initialization
	void Update () {
		if (!MainMenu.startTutorialAnimation || animationIsStarted) {
			return;
		}
		animationIsStarted = true;
		if (GameMaster.levelName == "Level20") {
			PlayAnimation (SlideFingerAnimationPrefab);
		} 
	}

	void OnTriggerStay(Collider other) {

		if (GameMaster.levelName == "Level1") {
			var changeDirection = other.GetComponent<ChangeDirection> ();
			if(changeDirection == null){
				return;
			}
			if( !changeDirection.isDragged && isSamePosition(changeDirection.transform.position)){
				if(!clickGateAnimation && changeDirection.currentDirection != DirectionEnum.Down){
					clickGateAnimation = true;
					clickPlayAnimation = false;
					slideFingerAnimation = false;					
					PlayCurrentAnimation();
				}else if(!clickPlayAnimation && changeDirection.currentDirection == DirectionEnum.Down){				
					clickGateAnimation = false;
					clickPlayAnimation = true;
					slideFingerAnimation = false;
					PlayCurrentAnimation();
				}
			}else if (!slideFingerAnimation && !isSamePosition(changeDirection.transform.position)){
				slideFingerAnimation = true;
				clickGateAnimation = false;
				clickPlayAnimation = false;
				PlayCurrentAnimation();
			}
		}else if(GameMaster.levelName == "Level20"){
			var changeColorGate = other.GetComponent<ChangeColorGate> ();
			if(changeColorGate == null){
				return;
			}
			if(!changeColorGate.isDragged && isSamePosition(changeColorGate.transform.position)){
				if(changeColorGate.CurrentTexture == changeColorGate.ProjectileTexture2){
					clickGateAnimation = false;
					slideFingerAnimation = false;
					PlayCurrentAnimation();
				}
				else if(!clickGateAnimation && changeColorGate.CurrentTexture != changeColorGate.ProjectileTexture2){
					clickGateAnimation = true;
					slideFingerAnimation = false;
					PlayCurrentAnimation();
				}
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (GameMaster.levelName == "Level1") {
			clickGateAnimation = false;
			clickPlayAnimation = false;
			slideFingerAnimation = true;
			PlayCurrentAnimation();
		}else if (GameMaster.levelName == "Level20"){
			slideFingerAnimation = true;
			clickGateAnimation = false;
			PlayCurrentAnimation();
		}
	}

	public void PlayCurrentAnimation(){
		EndAllAnimations();
		if (clickPlayAnimation) {
			PlayAnimation (ClickPlayAnimationPrefab);
		}
		else if (clickGateAnimation) {
			PlayAnimation (ClickFingerAnimationPrefab);
		}else if(slideFingerAnimation){
			PlayAnimation(SlideFingerAnimationPrefab);
		}
	}

	private bool isSamePosition(Vector3 pos){
		if ((Mathf.Round(pos.x * 10f) / 10f) != this.transform.position.x) {
			return false;
		}if ((Mathf.Round(pos.y * 10f) / 10f) != this.transform.position.y) {
			return false;
		}

		return true;
	}

	public void PlayAnimation(GameObject prefab){
		var instantce = Instantiate (prefab, prefab.transform.position, Quaternion.identity) as GameObject;
	}
	
	public void EndAnimation(GameObject instantce){
		Destroy(instantce);
	}

	public void EndAllAnimations(){
		var tutorials = GameObject.FindGameObjectsWithTag("Tutorial");		
		foreach (var tutorial in tutorials) {
			Destroy(tutorial);
		}
	}
}
