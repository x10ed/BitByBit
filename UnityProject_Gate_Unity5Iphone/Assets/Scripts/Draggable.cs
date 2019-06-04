using Assets.Scripts;
using Assets.Scripts.Common;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AssemblyCSharp;

[RequireComponent (typeof (BoxCollider))]
[RequireComponent (typeof (Rigidbody))]
public class Draggable : Disableable {

	private Vector3 screenPoint;
	private Vector3 offset;
	public Material snapInMaterial;
	private Vector3 snapInPosition;
	public Vector3 _vectorDirection;
	private IList<Transform> SnapOnList = new List<Transform>();
	private IList<Transform> OtherGameObjects = new List<Transform>();
	private Transform LastPosition;

	void OnMouseDown(){
		if (IsDisabledStartAnimation){return;}
		if(this.tag == "Template") { return; }
        if (IsDisabled){return;}
		MainMenu.GatesPosition.Remove(this.transform.position);
		screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x,Input.mousePosition.y,screenPoint.z));
        ScoreBoard.UpdateLabel();
	}

	void OnMouseUp(){
		if (IsDisabledStartAnimation){return;}
		if(this.tag == "Template") { return; }
        if (IsDisabled) { return; }
		FindClosestSnapOn();

		if (LastPosition == null || SnapOnList.Count == 0) {
            GameMaster.GetScoreBoard().CountDestroy(this.gameObject);
			Destroy(this.gameObject);			
			MainMenu.GatesPosition.Remove(this.transform.position);
		}

		//Eemaldame destroytud elemendid
		var toRemove = OtherGameObjects.Where (x => x == null).ToList();
		if (toRemove.Any ()) {
			foreach(var objectToRemove in toRemove){
				OtherGameObjects.Remove(objectToRemove);
			}
		}

        ScoreBoard.UpdateLabel();
	}
	
	void OnMouseDrag(){
		if (IsDisabledStartAnimation){return;}
        if (IsDisabled) { return; }
		if(this.tag == "Template") { return; }
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		//transform.position = curPosition;
		
		//et peale väljakule dragimist uuesti dragides ei jääks taust teiste gatede taha
		transform.position = new Vector3(curPosition.x,curPosition.y,-1);
	}

	void OnTriggerEnter(Collider other) {
        if (other.tag == "Ball" || other.tag == "Template") { return; }
        if (IsDisabled) { return; }
		if (SnapOnList == null) {
			SnapOnList = new List<Transform>();
		}
		if (OtherGameObjects == null) {
			OtherGameObjects = new List<Transform>();
		}
		if (HasSameMaterialName(other,snapInMaterial.name) && !SnapOnList.Any(x => x.position == other.transform.position)) {
			SnapOnList.Add(other.transform);
		} else if(other.tag == "Gate"){
			OtherGameObjects.Add(other.transform);
		}
	}

	void OnTriggerExit(Collider other){
        if (other.tag == "Ball" || other.tag == "Template") { return; }
        if (IsDisabled) { return; }
		if (SnapOnList != null && HasSameMaterialName(other,snapInMaterial.name)) {
			SnapOnList.Remove(other.transform);
		} else if(OtherGameObjects != null) {
			OtherGameObjects.Remove(other.transform);
		}
	}

	bool HasSameMaterialName(Collider obj, string materialName){
		if (obj == null ){ return false;}
		if (obj.GetComponent<Renderer>() == null ){ return false;}
		if (obj.GetComponent<Renderer>().sharedMaterial == null ){ return false;}
		
		string objectMaterialName = obj.GetComponent<Renderer>().material.name.Replace(" (Instance)","");

		return materialName == objectMaterialName;
	}

	void FindClosestSnapOn(){
		var nearestDistanceSqr = Mathf.Infinity;
		Transform nearestObj = null;

		foreach (var obj in SnapOnList) {
			
			var objectPos = obj.position;
			var distanceSqr = (objectPos - transform.position).sqrMagnitude;
			if (distanceSqr < nearestDistanceSqr && DoesntHaveAGate(objectPos)) {
				nearestObj = obj;
				nearestDistanceSqr = distanceSqr;
			}
		}
		if(nearestObj != null){
			transform.position = nearestObj.position;
			MainMenu.GatesPosition.Add(transform.position);
			LastPosition = nearestObj;
		}else if(LastPosition != null){
			transform.position = LastPosition.position;
		}
	}

	bool DoesntHaveAGate(Vector3 gatePosition){
		return !MainMenu.GatesPosition.Any(x => x != null && IsSamePosition(x,gatePosition));
	}

	bool IsSamePosition(Vector3 position1,Vector3 position2){
		
		bool isSame = true;
		if (Mathf.Abs(Mathf.Round(position1.x * 10f) - Mathf.Round (position2.x * 10f)) >= 1) {
			isSame = false;
		}
		
		if (Mathf.Abs(Mathf.Round (position1.y * 10f) - Mathf.Round (position2.y * 10f)) >= 1) {
			isSame = false;
		}
		return isSame;
	}
}
