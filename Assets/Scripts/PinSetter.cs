using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {
	
	public float distToRaise = 40f;
	public GameObject pinSet;
	
	private Animator anim;
	private PinCounter pinCounter;
	
	void Start () {
		anim = GetComponent<Animator>();
		pinCounter = FindObjectOfType<PinCounter>();
	}
	
	void Update () {
		
	}

	public void PerformAction(ActionMaster.Action action) {
		if(action == ActionMaster.Action.Tidy) {
			anim.SetTrigger("tidyTrigger");
		} else if(action == ActionMaster.Action.EndTurn) {
			anim.SetTrigger("resetTrigger");
			pinCounter.Reset();
		} else if(action == ActionMaster.Action.Reset) {
			anim.SetTrigger("resetTrigger");
			pinCounter.Reset();
		} else if(action == ActionMaster.Action.EndGame) {
			throw new UnityException("End Game not implemented");
		}
	}

	public void RaisePins() {
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			pin.Raise(distToRaise);
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

	void OnTriggerExit(Collider other) {
		GameObject thingLeft = other.gameObject;
		if(thingLeft.GetComponent<Pin>()) {
			Destroy(thingLeft);
		}
	}
}