using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class WorldScreenTexture : MonoBehaviour {

	public Texture LockedTexture;

	// Use this for initialization
	void Start () {
		if (!MainMenu.IsFullVersion ()) {
			this.transform.GetComponent<Renderer>().material.mainTexture = LockedTexture;
		}
	}
}
