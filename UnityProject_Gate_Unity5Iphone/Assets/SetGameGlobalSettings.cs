using UnityEngine;
using System.Collections;
using Assets.Scripts.Common;
using Assets.Scripts;

public class SetGameGlobalSettings : MonoBehaviour {

	public bool IsSchoolVersion;
	public bool IsFreeVersion;
	public float AdvertiseBreakeTime;
	public bool ShowAdds;
	// Use this for initialization
	void Start () {
		if (IsSchoolVersion) {
			MainMenu.SetIsSchoolVersion();
		}
		if (IsFreeVersion) {
			MainMenu.SetIsFreeVersion();
		}
		if(ShowAdds){
			MainMenu.SetShowAdds();
		}
		GameMaster.AdvertisePause = AdvertiseBreakeTime;
	}
}
