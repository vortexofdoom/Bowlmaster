using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {

	public float standThreshold = 5f;

	private Rigidbody rigidBody;

	void Start () {
		rigidBody = GetComponent<Rigidbody>();
	}

	public bool IsStanding() {
		//Vector3 rotationEuler = transform.rotation.eulerAngles;
		//float tiltX = Mathf.Abs(Quaternion.Angle(transform.rotation, Quaternion.AngleAxis(0f, Vector3.up)));
		//float tiltZ = Mathf.Abs(Mathf.DeltaAngle(rotationEuler.z, 0));
		//print(tiltX + " " + tiltZ);

		if(Mathf.Abs(Quaternion.Angle(transform.rotation, Quaternion.AngleAxis(0f, Vector3.up))) < standThreshold) {
			return true;
		} else {
			return false;
		}
	}

	public void Raise(float distance) {
		if(IsStanding()) {
			rigidBody.useGravity = false;
			transform.Translate(new Vector3(0, distance, 0), Space.World);
		}
	}

	public void Lower(float distance) {
		transform.Translate(new Vector3(0, -distance, 0), Space.World);
		rigidBody.useGravity = true;
	}

	void Update () {
	
	}
}