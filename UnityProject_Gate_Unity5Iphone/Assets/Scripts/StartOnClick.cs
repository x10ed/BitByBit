using UnityEngine;
using System.Collections;
using Assets.Scripts.Common;
using Assets.Scripts;

public class StartOnClick : MonoBehaviour {

	public Texture StartTexture;
	public Texture ResetTexture;
	bool start = true;
	private Animation resetBlinkAnim;
	//bool doCheck;
	//MainMenu mainMenu;

	void Start(){
		resetBlinkAnim = GetComponent<Animation> ();
		StopBlink ();
	}

	void Update(){
		if (MainMenu.isLoosing) {
			PlayBlink();
		}
	}

	public void PlayBlink(){		
		resetBlinkAnim.Play(resetBlinkAnim.clip.name);
	}

	public void StopBlink(){		
		MainMenu.isLoosing = false;
		resetBlinkAnim.Stop(resetBlinkAnim.clip.name);
	}

	public Animator GetAnimator(){
		Animator animator = this.gameObject.GetComponent<Animator>();
		return animator;		
	}

	void OnMouseUp(){
		if (MainMenu.animatingTileCount > 0) {
			return;
		}
		
		StopBlink();
		if (start) {
			MainMenu.StartGame();
			DisableGates();
		} 
		else {	
			GameMaster.GetMainMenu().Reset();
			GameMaster.EnableGates();
		}
		start = !start;
		ToggleButton();
	}

	void ToggleButton(){
		if (start) {
			transform.GetComponent<Renderer>().material.mainTexture = StartTexture;
		} else {
			transform.GetComponent<Renderer>().material.mainTexture = ResetTexture;
		}
	}

	IEnumerator ResetStartButton(){			
		//doCheck = false;	
		yield return new WaitForSeconds(2.0f);
		start = true;
		GameMaster.GetMainMenu().Reset();
		ToggleButton ();
		GameMaster.EnableGates();
	}

	void DisableGates(){

		var disableds = Resources.FindObjectsOfTypeAll (typeof(Disableable)) as Disableable[];

		if(disableds != null){
			foreach (var disabled in disableds) {
				if(disabled.gameObject.GetComponent<StartOnClick>() != null){
					continue;
				}
				if(disabled.gameObject.GetComponent<ShowLevelSelectBtn>() != null){
					continue;
				}

				disabled.IsDisabled = true;
			}
		}

		var baseGates = Resources.FindObjectsOfTypeAll (typeof(BaseGate)) as BaseGate[];
		
		if(baseGates != null){
			foreach (var baseGate in baseGates) {
				baseGate.IsDisabled = true;
			}
		}

		var animationHandlers = Resources.FindObjectsOfTypeAll (typeof(AnimationHandler)) as AnimationHandler[];
		
		if(baseGates != null){
			foreach (var animationHandler in animationHandlers) {
				if(!animationHandler.gameObject.activeInHierarchy){
					continue;
				}
				animationHandler.EndAllAnimations();
			}
		}
	}

	public void ResetStartTexture(){
		start = true;
		ToggleButton();
	}
}	
