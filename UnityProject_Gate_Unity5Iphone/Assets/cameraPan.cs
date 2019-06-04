using UnityEngine;
using System.Collections;

public class cameraPan : MonoBehaviour {

	public float mouseSensitivity = 1;
	private Vector3 lastPosition;

	void Update(){

		if (Input.GetMouseButtonDown(0))
		{
			lastPosition = Input.mousePosition;
		}
		
		if (Input.GetMouseButton(0))
		{
			Vector3 pan = Input.mousePosition - lastPosition;
			transform.Translate(-pan.x * mouseSensitivity, -pan.y * mouseSensitivity, 0);
			lastPosition = Input.mousePosition;
		}
	}
}
