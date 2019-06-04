using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public string groupName;
	public int PublicInstanceID;
 
	// Use this for initialization
	void Start () {
		PublicInstanceID = this.GetInstanceID();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
