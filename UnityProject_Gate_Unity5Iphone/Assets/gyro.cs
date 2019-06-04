using UnityEngine;
using System.Collections;

public class gyro : MonoBehaviour {

	public float gyroSpeedx;
	public float gyroSpeedy;

	Quaternion initRotation;

	void Start () {
		Input.gyro.enabled = true;

		initRotation = this.transform.rotation;
	}
	
	void Update() {

		transform.rotation = Quaternion.Lerp (this.transform.rotation, initRotation, 0.1f);
		this.transform.Rotate (-Input.gyro.rotationRateUnbiased.x * gyroSpeedx, -Input.gyro.rotationRateUnbiased.y * gyroSpeedy, 0);

	}
}
