using Assets.Scripts;
using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class ChangeDirection : BaseGate {
	public Texture UpTexture;
	public Texture DownTexture;
	public Texture LeftTexture;
	public Texture RightTexture;	
	public AudioClip onDirectionChangeAudio;
	private DirectionEnum nextDirection = DirectionEnum.Right;
	public DirectionEnum currentDirection = DirectionEnum.Up;
	float clickStart = 0;
	public Vector3 _vectorDirection;
	public bool isDragged;
	private Vector3 currentPosition;
	private Vector3 finalPosition;
	
	// Use this for initialization
	void Start () {
		_vectorDirection = new Vector3 (0, 1, 0);	
	}

    void Update() {
		isDragged =  !CompareApproximate(currentPosition,transform.position);
    }

	void OnMouseDown(){
		if (IsDisabledStartAnimation){return;}		
		if(this.tag == "Template") { return; }
        if (IsDisabled) { return; }
		isDragged = false;
		clickStart = Time.time;
		currentPosition = transform.position;
	}

	void OnMouseUp(){
		if (IsDisabledStartAnimation){return;}
        if (IsDisabled) { return; }

		finalPosition = transform.position;
		float differenceX = Mathf.Abs (finalPosition.x - currentPosition.x);
		float differenceY = Mathf.Abs (finalPosition.y - currentPosition.y);

		currentPosition = transform.position;

		if((differenceX < 1) && (differenceY < 1)){
		//if (!isDragged) {
			if ((Time.time - clickStart) < 2.0f) {

				this.OnClick();
				clickStart = -1;
			}
		}


	}

	public bool CompareApproximate(Vector3 a, Vector3 b)
	{
		if(!Mathf.Approximately(a.x, b.x))
			return false;
		if(!Mathf.Approximately(a.y, b.y))
			return false;
		return true;
	}

	void OnMouseDrag(){
        if (IsDisabled) { return; }
	}
	
	void OnClick()
	{
		var directionTemp = nextDirection;
		
		switch (directionTemp)
		{
		case DirectionEnum.Right: // Paremale
			_vectorDirection = new Vector3 (1,0,0);
			transform.GetComponent<Renderer>().material.mainTexture = RightTexture;
			currentDirection = DirectionEnum.Right;
			nextDirection = DirectionEnum.Down;
			break;
		case DirectionEnum.Down: // Alla
			_vectorDirection = new Vector3 (0,-1,0);
			transform.GetComponent<Renderer>().material.mainTexture = DownTexture;
			currentDirection = DirectionEnum.Down;
			nextDirection = DirectionEnum.Left;
			break;
		case DirectionEnum.Left: // Vasakule
			_vectorDirection = new Vector3 (-1,0,0);	
			transform.GetComponent<Renderer>().material.mainTexture = LeftTexture;
			currentDirection = DirectionEnum.Left;
			nextDirection = DirectionEnum.Up;
			break;
		default: // Üles
			_vectorDirection = new Vector3 (0,1,0);
			transform.GetComponent<Renderer>().material.mainTexture = UpTexture;
			currentDirection = DirectionEnum.Up;
			nextDirection = DirectionEnum.Right;
			break;
		}
		AudioSource.PlayClipAtPoint (onDirectionChangeAudio, transform.position);
	}
}
