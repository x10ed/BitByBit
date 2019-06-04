using Assets.Scripts;
using Assets.Scripts.Common;
using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Ball : MonoBehaviour {

	Vector3 myVector = new Vector3 (1,0,0);
	public DirectionEnum CurrentDirection = DirectionEnum.Right;
	public ColorEnum Color;
	public BallSizeEnum Size;
	public bool go = false;
	public AudioClip DeathSound;
	public AudioClip JumpSound;
	private Vector3? animationEndPosition;
	public Material snapIn;
	public bool activateBehaviour = true;
	bool destoryObject = false;
	bool playDeathSound = false;
	bool destroyOnGun = false;
	int TelePortExtiGateInstanceID;
	public AnimationClip animDie;
	public AnimationClip animFinish;
	public AudioClip TeleportSound;
	public bool isJumping;

	public Animator GetAnimator(){
		Animator animator = null;
		foreach(Transform childObj in transform){
			if (childObj.name == "animation"){
				animator = childObj.gameObject.GetComponent<Animator>();
			}
		}
		return animator;
		
	}
	void Start(){
		transform.position = getPosition();
	}

	Vector3 getPosition(){
		return new Vector3(this.transform.position.x,this.transform.position.y, this.transform.position.y / 10);
	}

	void Update () {
        /////////////////////////7
        RaycastHit hit;
        var direction = Vector3.forward;
        if (activateBehaviour && !Physics.Raycast(transform.position, direction, out hit, 10) ) {
            go = false;
            activateBehaviour = false;
            DestroyGameObject();
  
        }
        /////////////////////////////////////


	    if (go) {
	        if (tag != "Template") {
	            if (!destoryObject) {
	                if (playWalkforFirstTime) {
	                    Play();
	                    playWalkforFirstTime = false;
	                }
	                transform.Translate(myVector*2*Time.deltaTime);
	                transform.position = getPosition();
	            }
	            if (animationEndPosition != null) {
	                if (IsSamePosition(animationEndPosition.Value)) {
	                    Play(CurrentDirection);
	                    animationEndPosition = null;
	                    activateBehaviour = true;
						isJumping = false;
	                }
	            }
	            if (destoryObject) {
	                DestroyGameObject();
	            }
	        }
	    }
	    else {
	        if (playIdleforFirstTime) {
                PlayIdle();
	            playIdleforFirstTime = false;
	        }

	    }
	}

    private bool playIdleforFirstTime = true;
    private bool playWalkforFirstTime = true;

	public void SetIdelFalse(){
		playIdleforFirstTime = false;
	}

    private void PlayIdle() {

        var direction = this.CurrentDirection;
        var animator = GetAnimator();
        if (animator == null) {
            return;
        }
        switch (direction) {
            case DirectionEnum.Right: // Paremale
                animator.Play("IdleRight");
                break;
            case DirectionEnum.Down: // Alla			
                animator.Play("IdleFront");
                break;
            case DirectionEnum.Left: // Vasakule			
                animator.Play("IdleLeft");
                break;
            default: // Üles			
                animator.Play("IdleBack");
                break;
        }

    }

	void OnTriggerEnter(Collider other) {
		if (activateBehaviour && !destoryObject && go) {
			var wall = other.GetComponent<Wall> ();

			if (wall != null && (wall.IgnoreBounceColor != Color) && activateBehaviour) {
				TurnAround();
				return;
			}	
			
			var door = other.GetComponent<Door> ();
			if (door != null && activateBehaviour) {
				if(door.Color != Color){
					TurnAround();
					return;
				}else{
					door.DestroyGate();
				}
			}	

			var destroy = other.GetComponent<Destroy> ();
			if (destroy != null) {
				destoryObject =  tag != "Template";
			}
		}
	}
	
	void OnTriggerStay(Collider other){
		if (activateBehaviour && !destoryObject && go){
			if(IsSamePosition(other.transform.position)){
				var draggable = other.GetComponent<ChangeDirection> ();
				if(draggable != null){
					//Hetkel kui on sama värvi, siis pöörab sinna kuhu on määratud
					//Kui on teist värvi, siis läheb läbi
					if (IsCorrectGate(draggable)){
						transform.position = new Vector3(other.transform.position.x,other.transform.position.y,transform.position.z);
						Rotate(draggable._vectorDirection,draggable.currentDirection);
						activateBehaviour = false;
					}
				}
				//Kui on tegemist topel väravaga, siis pole mõtet kontrollida materiali
				var doubleDraggable = other.GetComponent<ChangeDirectionDouble>();
				if(doubleDraggable != null){
					transform.position = new Vector3(other.transform.position.x,other.transform.position.y,transform.position.z);
					Rotate(doubleDraggable.GetVectorDirection(Color),doubleDraggable.GetDirectionEnum(Color));
					activateBehaviour = false;
				}
				var jump = other.GetComponent<Jump>();
				if(jump != null && (IsCorrectGate(jump) || jump.IsNeutral)){	
					animationEndPosition = jump.GetNewPosition(this.CurrentDirection, transform.position);
					Jump(CurrentDirection);
					activateBehaviour = false;
					isJumping = true;
				}
				var size = other.GetComponent<SizeGate>();
				if(size != null){				
					this.Size = size.ToSize;
				    Play();
				}
	            var changeColorGate = other.GetComponent<ChangeColorGate>();
	            if (changeColorGate != null) {
	                activateBehaviour = false;
	                changeColorGate.DoColorChange(this);
	                //Play();
	            }	
				var changeDirectionStatic = other.GetComponent<StaticChangeDirection> ();
				if (changeDirectionStatic != null && (IsCorrectGate(changeDirectionStatic) || changeDirectionStatic.IsNeutral)) {
						transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, transform.position.z);
						Rotate (changeDirectionStatic.GetVectorDirection (), changeDirectionStatic.currentDirection);
						activateBehaviour = false;
				}
				var teleportGate = other.GetComponent<Teleport> ();
				if (teleportGate != null) {
						activateBehaviour = false;
						var teleportGates = GameObject.FindGameObjectsWithTag ("Teleport");
						foreach (var tG in teleportGates) {
								var tGate = tG.transform.GetComponent<Teleport> ();
							if(tGate == null){
								continue;
							}
							if (teleportGate.groupName == tGate.groupName && teleportGate.PublicInstanceID != tGate.PublicInstanceID) {
								TelePortExtiGateInstanceID = tGate.PublicInstanceID;
								activateBehaviour = false;
								AudioSource.PlayClipAtPoint (TeleportSound, transform.position);
								transform.position = new Vector3 (tGate.transform.position.x, tGate.transform.position.y, transform.position.z);
								break;
							}
						}	
				}
				var finish = other.GetComponent<FinishCounter>();
				if (finish != null) {
					if(finish.IsCorrectObject(this.GetComponent<Collider>())){
						var animator = GetAnimator ();
						if (animator == null) {
							return;
						}
						go = false;
						animator.Play("Finish");

						//tekitab finishi particle
						GameObject finishParticle = Instantiate(Resources.Load("prefabs/finishParticle", typeof(GameObject)), new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 2), Quaternion.identity) as GameObject;

						Destroy (this.gameObject, animFinish.length);
                        IpiCounter.Minus();
						MainMenu mainMenu = GameMaster.GetMainMenu ();
						mainMenu.SetMinusOne();
					}
				}				
				var colorWall= other.GetComponent<ColorWall> ();
				if (colorWall != null && IsCorrectGate(colorWall)) {
					TurnAround();
					activateBehaviour = false;
				}
			}
			
			var destroy = other.GetComponent<Destroy> ();
			if (destroy != null) {
				destoryObject =  tag != "Template";
			}
			var gun = other.GetComponent<Gun>();
			if(gun != null){
				destoryObject = destroyOnGun && tag != "Template";
			}
		} 
	}

	bool IsCorrectGate(BaseGate gate){
		return Color == gate.Color;
	}

	void OnTriggerExit(Collider other){

		if (!go){ return; }
		if (isJumping) { return; }

		var draggable = other.GetComponent<ChangeDirection> ();
		if (draggable != null && IsCorrectGate(draggable)) {
			activateBehaviour = true;
			return;
		}
		var doubleDraggable = other.GetComponent<ChangeDirectionDouble> ();
		if (doubleDraggable != null) {
			activateBehaviour = true;
			return;
		}
		var gun = other.GetComponent<Gun> ();
		if (gun != null) {
			//Et sündides kohe ei hävitaks
			destroyOnGun = true;
		}
		var changColor = other.GetComponent<ChangeColorGate> ();
		if (changColor != null) {
			activateBehaviour = true;
			return;
		}
		var changeDirectionStatic = other.GetComponent<StaticChangeDirection> ();
		if (changeDirectionStatic != null) {
			activateBehaviour = true;
			return;
		}

		var teleportGate = other.GetComponent<Teleport> ();
		if(teleportGate != null && TelePortExtiGateInstanceID == teleportGate.PublicInstanceID){
			TelePortExtiGateInstanceID = 0;
			activateBehaviour = true;
			return;
		}
		var colorWall= other.GetComponent<ColorWall> ();
		if (colorWall != null) {
			activateBehaviour = true;
			return;
		}
		var finishCounter = other.GetComponent<FinishCounter> ();
		if (finishCounter != null) {
			activateBehaviour = true;
			return;
		}
	}
	
	bool IsSamePosition(Vector3 position){
		
		bool isSame = true;
		if (Mathf.Abs(Mathf.Round(position.x * 10f) - Mathf.Round (transform.position.x * 10f)) >= 3) {
			isSame = false;
		}
		
		if (Mathf.Abs(Mathf.Round (position.y * 10f) - Mathf.Round (transform.position.y * 10f)) >= 3) {
			isSame = false;
		}
		return isSame;
	}

	public void Rotate(DirectionEnum textureDirection){
		Rotate (GetVector(textureDirection), textureDirection);
	}

	public 	Vector3 GetVector(DirectionEnum direction){
		switch (direction)
		{
		case DirectionEnum.Right: // Paremale
			return new Vector3 (1,0,0);
		case DirectionEnum.Down: // Alla
			return new Vector3 (0,-1,0);
		case DirectionEnum.Left: // Vasakule
			return new Vector3 (-1,0,0);
		default: // Üles
			return new Vector3 (0,1,0);
		}
	}
	
	public void Rotate(Vector3 direction, DirectionEnum directionEnum){		
		myVector = direction;
		CurrentDirection = directionEnum;
		Play(directionEnum);
	}

	public void Play(){
		Play(this.CurrentDirection);
	}

	public void Play(DirectionEnum? direction){
	    if (direction == null){
	        direction = this.CurrentDirection;
	    }
		var animator = GetAnimator ();
		if (animator == null) {
			return;
		}
		switch (direction)
		{
			case DirectionEnum.Right: // Paremale
				animator.Play("WalkRight");
				break;
			case DirectionEnum.Down: // Alla			
				animator.Play("WalkDown");
				break;
			case DirectionEnum.Left: // Vasakule			
				animator.Play("WalkLeft");
				break;
			default: // Üles			
				animator.Play("WalkUp");;
				break;
		}
	}

	public void Jump(DirectionEnum direction){
		var animator = GetAnimator ();
		if (animator == null) {
			return;
		}
		AudioSource.PlayClipAtPoint (JumpSound, transform.position);
		switch (direction)
		{
		case DirectionEnum.Right: // Paremale
			animator.Play("JumpRight");
			break;
		case DirectionEnum.Down: // Alla			
			animator.Play("JumpDown");
			break;
		case DirectionEnum.Left: // Vasakule			
			animator.Play("JumpLeft");
			break;
		default: // Üles			
			animator.Play("JumpUp");;
			break;
		}
	}
	
	public void TurnAround(){
		switch (CurrentDirection)
		{
		case DirectionEnum.Right: // Paremale
			Rotate(DirectionEnum.Left);
			break;
		case DirectionEnum.Down: // Alla
			Rotate(DirectionEnum.Up);
			break;
		case DirectionEnum.Left: // Vasakule	
			Rotate(DirectionEnum.Right);
			break;
		case DirectionEnum.Up: // Üles
			Rotate(DirectionEnum.Down);
			break;
		}
	}
		
	void DestroyGameObject(){
		SpriteRenderer sprite = null;
		if (!playDeathSound) {
			AudioSource.PlayClipAtPoint (DeathSound, transform.position);
			playDeathSound = !playDeathSound;
		}
		foreach(Transform childObj in transform){
			if (childObj.name == "animation"){
				sprite = childObj.gameObject.GetComponent<SpriteRenderer>();
				if(sprite != null){
					var animator = GetAnimator ();
					if (animator == null) {
						return;
					}
					animator.Play("Fall");
					Destroy (this.gameObject, animDie.length);

				}
			}
		}
        IpiCounter.Minus();

	}

}
