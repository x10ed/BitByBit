using UnityEngine;
using System.Collections;

public class OnStartDeletePlayerPrefs : MonoBehaviour {
    public bool DeleteOnFirstStart;
	// Use this for initialization
	void Start () {
	    if (!DeleteOnFirstStart) {
            PlayerPrefs.DeleteAll();
	        DeleteOnFirstStart = true;
	    }
	    
	}

}
