using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Ball ball;

	private Vector3 offset;
	
	void Start () {
		offset = transform.position - ball.transform.position;
	}
	
	void Update () {
		if (ball.transform.position.z < 1829) {
			transform.position = ball.transform.position + offset;
		}
	}
}