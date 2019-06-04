using System;
using AssemblyCSharp;
using Assets.Scripts;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour,IReset {

	bool go = false;
	bool isWhileLoopRunning = false;
	public Rigidbody projectile;
	public Rigidbody projectile2;
	public Vector3 position;
	int Projectile1CurrentCount;
	public int Projectile1InitialCount;
	int Projectile2CurrentCount;
	public int Projectile2InitialCount;
	public AssemblyCSharp.DirectionEnum direction;

  Vector2 GetSpriteOffset() {
    if (direction == DirectionEnum.Down)
      return new Vector2(0, 0);
    if (direction == DirectionEnum.Up)
      return new Vector2(0.25f, 0);
    if (direction == DirectionEnum.Left)
      return new Vector2(0.5f, 0);
    if (direction == DirectionEnum.Right)
      return new Vector2(0.75f, 0);
    return new Vector2(0, 0);;
  }
	int temp_debugNumber = 0 ;
  void Awake() {
      //renderer.material.mainTextureOffset = GetSpriteOffset();
      if (GetComponent<Renderer>() != null) {
          if (GetComponent<Renderer>().sharedMaterial != null) {
              GetComponent<Renderer>().sharedMaterial.mainTextureOffset = GetSpriteOffset();
          }
      }
  }

    void Start() {
		Reset ();
		SetTexture();
	}

	public void SetGo(bool value){
		go = value;
		if (!isWhileLoopRunning && go) {
			StartCoroutine(Fire());
			isWhileLoopRunning = true;
		}
	}

	IEnumerator Fire(){
		while(true)	{
			//GameObject template = GameObject.Find ("BallTemplate");
			if (go && (Projectile1CurrentCount > 0 || Projectile2CurrentCount > 0)){
				Rigidbody clone;
				var projectileVar = GetProjectile();
                if (projectileVar != null) {

                    var newPos = this.transform.position;
                    newPos.z = projectileVar.transform.position.z + position.z; // projektiili puhul z jätame samaks
                    newPos.y += position.y;
                    newPos.x += position.x;

                    clone = Instantiate(projectileVar, newPos, transform.rotation) as Rigidbody;
					clone.name = clone.name + temp_debugNumber++;
					var nameVar = clone.GetComponent<Renderer>().material.name; // cloonimise unity kiiks - kui materjali ei küsi siis seda ei cloonita 
                    clone.tag = "Ball";
					Ball ballComponent = clone.GetComponent(typeof(Ball)) as Ball;
					if (ballComponent != null){
						ballComponent.Rotate(direction);
					}
				}
			}
			yield return new WaitForSeconds(2);
		}
	}


	public void Reset(){
		this.Projectile1CurrentCount = this.Projectile1InitialCount;
		this.Projectile2CurrentCount = this.Projectile2InitialCount;
	}

	Rigidbody GetProjectile(){
		if (Projectile1CurrentCount == 0 && Projectile2CurrentCount ==0 ) {
			return null;
		}


		if (Projectile1CurrentCount == 0 && Projectile2CurrentCount > 0 ) {
			Projectile2CurrentCount--;
			return projectile2;	
		}

		if (Projectile2CurrentCount == 0 && Projectile1CurrentCount > 0 ) {
			Projectile1CurrentCount--;
			return projectile;	
		}

		var randomNumber = Random.Range (1, 3);// Random.Range with integer values will not include the upper limit.
		if (randomNumber == 1) {
			Projectile1CurrentCount--;
			return projectile;	
		} 
		else {
			Projectile2CurrentCount--;
			return projectile2;		
		}

	}

	private void SetTexture(){
		switch (direction)
		{
			case DirectionEnum.Right: // Paremale
			this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0.75f,0));
				break;
		case DirectionEnum.Down: // Alla			
			this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0f,0));
				break;
		case DirectionEnum.Left: // Vasakule			
			this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0.5f,0));
				break;
		default: // Üles			
			this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0.25f,0));
				break;
		}
	}
}
