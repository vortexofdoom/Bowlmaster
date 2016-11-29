using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {

	public int lastStandingCount = -1;
	public Text standingDisplay;
	public float distToRaise = 40f;
	public GameObject pinSet;
	
	
	private ActionMaster actionMaster = new ActionMaster();
	private Ball ball;
	private bool ballOutOfPlay = false;
	private float lastChangeTime;
	private int lastSettledCount = 10;
	private Animator anim;
	
	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
		anim = GetComponent<Animator>();
	}
	
	void Update () {
		standingDisplay.text = CountStanding().ToString();

		if(ballOutOfPlay) {
			CheckStanding();
			standingDisplay.color = Color.red;
		}
	}

	public void RaisePins() {
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			pin.Raise(distToRaise);
			pin.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}

	public void LowerPins() {
		foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			pin.Lower(distToRaise);
		}
	}

	public void RenewPins() {
		Instantiate(pinSet, new Vector3(0, 0, 1880), Quaternion.identity);
		RaisePins();
	}

	void CheckStanding() {
		int currentStanding = CountStanding();

		if (currentStanding != lastStandingCount) {
			lastChangeTime = Time.time;
			lastStandingCount = currentStanding;
		}

		float settleTime = 3f;
		if((Time.time - lastChangeTime) > settleTime) {
			PinsHaveSettled();
		}

	}

	void PinsHaveSettled() {
		int standing = CountStanding();
		int pinFall = lastSettledCount - standing;
		lastSettledCount = CountStanding();

		ActionMaster.Action action = actionMaster.Bowl(pinFall);
		Debug.Log("Pinfall: " + pinFall + ", " + action);

		if(action == ActionMaster.Action.Tidy) {
			anim.SetTrigger("tidyTrigger");
		} else if (action == ActionMaster.Action.EndTurn) {
			anim.SetTrigger("resetTrigger");
			lastSettledCount = 10;
		} else if (action == ActionMaster.Action.Reset) {
			anim.SetTrigger("resetTrigger");
			lastSettledCount = 10;
		} else if (action == ActionMaster.Action.EndGame) {
			throw new UnityException("End Game not implemented");
		}

		ball.Reset();
		lastStandingCount = -1;
		ballOutOfPlay = false;
		standingDisplay.color = Color.green;
	}

	int CountStanding() {
		int standing = 0;

		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			if (pin.IsStanding()) {
				standing++;
			}
		}
		return standing;
	}

	void OnTriggerExit(Collider other) {
		GameObject thingLeft = other.gameObject;
		if(thingLeft.GetComponent<Pin>()) {
			Destroy(thingLeft);
		}
	}

	public void SetBallOutOfPlay() {
		ballOutOfPlay = true;
	}
}