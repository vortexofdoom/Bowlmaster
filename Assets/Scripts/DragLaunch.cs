using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Ball))]
public class DragLaunch : MonoBehaviour {

	private Ball ball;
	private Vector3 dragStart, dragEnd;
	private float startTime, endTime;
	
	void Start () {
		ball = GetComponent<Ball>();
	}

	public void MoveStart(float amountX) {
		if(!ball.inPlay) {
			ball.transform.Translate(amountX, 0, 0);
		}
	}
	
	public void DragStart() {
		if(!ball.inPlay) {
			//Capture time & position of drag start
			dragStart = Input.mousePosition;
			startTime = Time.time;
		}
	}
	
	public void DragEnd() {
		if(!ball.inPlay) {
			//Launch ball
			dragEnd = Input.mousePosition;
			endTime = Time.time;

			float dragDuration = endTime - startTime;

			float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
			float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

			Vector3 launchVelocity = new Vector3(launchSpeedX, 0, launchSpeedZ);
			ball.Launch(launchVelocity);
		}
	}
}