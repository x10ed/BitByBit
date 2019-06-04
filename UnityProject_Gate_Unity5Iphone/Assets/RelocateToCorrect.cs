using UnityEngine;
using System.Collections;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts;
using Assets.Scripts.Common;

public class RelocateToCorrect : MonoBehaviour {
	public Vector3 position;
	// Use this for initialization
	void Start () {
		position = Camera.main.ViewportToWorldPoint(position);
		this.transform.position = new Vector3 (position.x, position.y, this.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
