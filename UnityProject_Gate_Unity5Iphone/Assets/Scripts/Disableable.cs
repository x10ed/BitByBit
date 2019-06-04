using AssemblyCSharp;
using Assets.Scripts;
using Assets.Scripts.Common;
using UnityEngine;
using System.Collections;

public class Disableable : MonoBehaviour {
    public bool IsDisabled;
    public ColorEnum Color;
    public bool hiddeLabel;	
	public bool IsDisabledStartAnimation;
	private int temp = -1;

	void Update(){
		IsDisabledStartAnimation = MainMenu.animatingTileCount > 0;
	}
}
