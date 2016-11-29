using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public Vector3 launchVelocity;
	public bool inPlay = false;

	private Rigidbody rigidBody;
	private AudioSource audioSource;
	private Vector3 startPosition;
	
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.useGravity = false;
		audioSource = GetComponent<AudioSource>();
		startPosition = transform.position;
	}

	public void Launch(Vector3 velocity) {
		inPlay = true;
		rigidBody.useGravity = true;
		rigidBody.velocity = velocity;
		audioSource.Play();
	}

	public void Reset() {
		rigidBody.useGravity = false;
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
		transform.position = startPosition;
		inPlay = false;
	}
}
