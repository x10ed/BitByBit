using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Assets.Scripts.Common;
public class ContinuesMusicPlayer : MonoBehaviour {
		
	public AudioClip BackgroundSound;
	public Texture Mute;
	public Texture Play;
	bool? isPlaying;

	void OnMouseUp(){		
		isPlaying = !isPlaying;
		SetSound ();
	}

	void Awake () {
		var CMP = GameObject.FindObjectOfType<ContinuesMusicPlayer> ();
		if (CMP != null && CMP != this) {
			CMP.SetSound();
			Destroy(this.gameObject);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
		
		if (isPlaying == null) {
			isPlaying = true;
		}	                    
		
		SetSound ();
	}

	public void SetSound(){
		if (isPlaying == false) {
			transform.GetComponent<Renderer>().material.mainTexture = Mute;
			MuteSound();
		} else {
			transform.GetComponent<Renderer>().material.mainTexture = Play;
			PlaySound();
		}
	}

	void MuteSound(){
		var listener = Camera.main.gameObject.GetComponent(typeof(AudioListener)) as AudioListener;
		listener.enabled = false;
	}

	void PlaySound(){
		var listener = Camera.main.gameObject.GetComponent(typeof(AudioListener)) as AudioListener;
		listener.enabled = true;
	}
}