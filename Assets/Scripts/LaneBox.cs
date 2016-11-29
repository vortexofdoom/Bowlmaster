using UnityEngine;
using System.Collections;

public class LaneBox : MonoBehaviour {

	private PinSetter pinSetter;

	void Start () {
		pinSetter = GameObject.FindObjectOfType<PinSetter>();
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.GetComponent<Ball>()){
			pinSetter.SetBallOutOfPlay();
		}
	}
}