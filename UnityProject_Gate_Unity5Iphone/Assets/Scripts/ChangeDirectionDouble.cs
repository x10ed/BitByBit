using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class ChangeDirectionDouble : MonoBehaviour {

	public Vector3 screenPosition;
	private bool isDown = false;
	public GUIStyle firstGate;
	public GUIStyle secondGate;

	private DirectionEnum firstGateNextDirection = DirectionEnum.Right;
	public DirectionEnum firstGateCurrentDirection = DirectionEnum.Up;

	private DirectionEnum secondGateNextDirection = DirectionEnum.Right;
	public DirectionEnum secondGateCurrentDirection = DirectionEnum.Up;
	
	public Vector3 firstVectorDirection;	
	public Vector3 secondVectorDirection;

	public ColorEnum firstColor;
	public ColorEnum secondColor;

	//For GUI textbox
	public Texture2D FirstUpTextureGUI;
	public Texture2D FirstDownTextureGUI;
	public Texture2D FirstLeftTextureGUI;
	public Texture2D FirstRightTextureGUI;
	public Texture2D SecondUpTextureGUI;
	public Texture2D SecondDownTextureGUI;
	public Texture2D SecondLeftTextureGUI;
	public Texture2D SecondRightTextureGUI;

	//For gate
	public Texture2D FirstUpTexture;
	public Texture2D FirstDownTexture;
	public Texture2D FirstLeftTexture;
	public Texture2D FirstRightTexture;
	public Texture2D SecondUpTexture;
	public Texture2D SecondDownTexture;
	public Texture2D SecondLeftTexture;
	public Texture2D SecondRightTexture;

	public AudioClip onDirectionChangeAudio;

	//So that in first click (clone event)
	//we don't show the conf box
	private bool showConfBox = false;

	void Start(){
		firstVectorDirection = new Vector3 (0, 1, 0);
		secondVectorDirection = new Vector3 (0, 1, 0);
	}

	void OnMouseDown(){
		screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - screenPosition.y;
		isDown = !isDown;
	}

	void OnMouseUp(){
		if (!showConfBox) {
			isDown = false;
			showConfBox = true;
		}
	}

	void OnGUI () {
		if (isDown && showConfBox) {
			GUI.Box (new Rect (screenPosition.x + 40, screenPosition.y - 65, 120, 65), "");
			if(GUI.Button(new Rect (screenPosition.x + 40, screenPosition.y - 65, 64, 64), "", firstGate)){
				OnClickFirst();
			}
			if(GUI.Button(new Rect (screenPosition.x + 94, screenPosition.y - 65, 64, 64), "", secondGate)){
				OnClickSecond();
			}
		}
	}

	void OnClickFirst()
	{
		var directionTemp = firstGateNextDirection;
		var gateTextureObject = this.transform.GetChild (0);

		switch (directionTemp)
		{
		case DirectionEnum.Right: // Paremale
			firstVectorDirection = new Vector3 (1,0,0);
			gateTextureObject.GetComponent<Renderer>().material.mainTexture = FirstRightTexture;
			firstGate.normal.background = FirstRightTextureGUI;
			firstGateCurrentDirection = DirectionEnum.Right;
			firstGateNextDirection = DirectionEnum.Down;
			break;
		case DirectionEnum.Down: // Alla
			firstVectorDirection = new Vector3 (0,-1,0);
			gateTextureObject.GetComponent<Renderer>().material.mainTexture = FirstDownTexture;
			firstGate.normal.background = FirstDownTextureGUI;
			firstGateCurrentDirection = DirectionEnum.Down;
			firstGateNextDirection = DirectionEnum.Left;
			break;
		case DirectionEnum.Left: // Vasakule
			firstVectorDirection = new Vector3 (-1,0,0);	
			gateTextureObject.GetComponent<Renderer>().material.mainTexture = FirstLeftTexture;
			firstGate.normal.background = FirstLeftTextureGUI;
			firstGateCurrentDirection = DirectionEnum.Left;
			firstGateNextDirection = DirectionEnum.Up;
			break;
		default: // Üles
			firstVectorDirection = new Vector3 (0,1,0);
			gateTextureObject.GetComponent<Renderer>().material.mainTexture = FirstUpTexture;
			firstGate.normal.background = FirstUpTextureGUI;
			firstGateCurrentDirection = DirectionEnum.Up;
			firstGateNextDirection = DirectionEnum.Right;
			break;
		}
		//audio.PlayOneShot(onDirectionChangeAudio);
	}

	void OnClickSecond()
	{
		var directionTemp = secondGateNextDirection;
		var gateTextureObject = this.transform.GetChild (1);
		
		switch (directionTemp)
		{
		case DirectionEnum.Right: // Paremale
			secondVectorDirection = new Vector3 (1,0,0);
			gateTextureObject.GetComponent<Renderer>().material.mainTexture = SecondRightTexture;
			secondGate.normal.background = SecondRightTextureGUI;
			secondGateCurrentDirection = DirectionEnum.Right;
			secondGateNextDirection = DirectionEnum.Down;
			break;
		case DirectionEnum.Down: // Alla
			secondVectorDirection = new Vector3 (0,-1,0);
			gateTextureObject.GetComponent<Renderer>().material.mainTexture = SecondDownTexture;
			secondGate.normal.background = SecondDownTextureGUI;
			secondGateCurrentDirection = DirectionEnum.Down;
			secondGateNextDirection = DirectionEnum.Left;
			break;
		case DirectionEnum.Left: // Vasakule
			secondVectorDirection = new Vector3 (-1,0,0);	
			gateTextureObject.GetComponent<Renderer>().material.mainTexture = SecondLeftTexture;
			secondGate.normal.background = SecondLeftTextureGUI;
			secondGateCurrentDirection = DirectionEnum.Left;
			secondGateNextDirection = DirectionEnum.Up;
			break;
		default: // Üles
			secondVectorDirection = new Vector3 (0,1,0);
			gateTextureObject.GetComponent<Renderer>().material.mainTexture = SecondUpTexture;
			secondGate.normal.background = SecondUpTextureGUI;
			secondGateCurrentDirection = DirectionEnum.Up;
			secondGateNextDirection = DirectionEnum.Right;
			break;
		}
		//audio.PlayOneShot(onDirectionChangeAudio);
	}

	public Vector3 GetVectorDirection(ColorEnum color){
		if (firstColor == color) {
			return firstVectorDirection;
		}
		if (secondColor == color) {
			return secondVectorDirection;
		}

		return firstVectorDirection;
	}

	public DirectionEnum GetDirectionEnum(ColorEnum color){
		if (firstColor == color) {
			return firstGateCurrentDirection;
		}
		if (secondColor == color) {
			return secondGateCurrentDirection;
		}
		
		return firstGateCurrentDirection;
	}
}
