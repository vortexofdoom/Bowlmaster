using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinCounter : MonoBehaviour {

	public Text standingDisplay;

	private bool ballOutOfPlay = false;
	private int lastStandingCount = -1;
	private int lastSettledCount = 10;
	private float lastChangeTime;

	private GameManager gameManager;

	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	
	void Update () {
		standingDisplay.text = CountStanding().ToString();

		if(ballOutOfPlay) {
			CheckStanding();
			standingDisplay.color = Color.red;
		}
	}

	void CheckStanding() {
		int currentStanding = CountStanding();

		if(currentStanding != lastStandingCount) {
			lastChangeTime = Time.time;
			lastStandingCount = currentStanding;
		}

		float settleTime = 3f;
		if((Time.time - lastChangeTime) > settleTime) {
			PinsHaveSettled();
		}

	}

	public void Reset() {
		lastSettledCount = 10;
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject.GetComponent<Ball>()) {
			ballOutOfPlay = true;
		}
	}

	void PinsHaveSettled() {
		int standing = CountStanding();
		int pinFall = lastSettledCount - standing;
		lastSettledCount = CountStanding();

		gameManager.Bowl(pinFall);

		lastStandingCount = -1;
		ballOutOfPlay = false;
		standingDisplay.color = Color.green;
	}

	int CountStanding() {
		int standing = 0;

		foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			if(pin.IsStanding()) {
				standing++;
			}
		}
		return standing;
	}
}