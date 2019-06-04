using Assets.Scripts;
using UnityEngine;
using System.Collections;

public interface ICondition{
	event ConditionChangeHandler ConditionChange;
}

public delegate void ConditionChangeHandler(bool state);

public class IfCount : MonoBehaviour ,ICondition{

	public event ConditionChangeHandler ConditionChange;


	public IReset reset;
	// Use this for initialization
	void Start () {
	
	}

	public bool state;
	// Update is called once per frame
	void Update () {
		if (i == 0) {
			state = true;
			if (ConditionChange != null){
				ConditionChange(state);
			}
			i = 9;
		}
	}


	public int i = 3;

	void OnTriggerEnter(Collider colliderVar) {
        if (colliderVar.tag == "Ball") {
						if (i > 0) {
								i--;
						}
				}

	}
}
