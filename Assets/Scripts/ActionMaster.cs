using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionMaster {
	public enum Action {Tidy, Reset, EndTurn, EndGame};

	private int[] bowls = new int[21];
	private int bowl = 1;

	public PinSetter pinSetter;

	public static Action NextAction(List<int> pinFalls) {
		ActionMaster am = new ActionMaster();
		Action currentAction = new Action();

		foreach (int pinFall in pinFalls) {
			currentAction = am.Bowl(pinFall);
		}

		return currentAction;
	}

	private Action Bowl(int pins) {
		if(pins < 0 || pins > 10) {throw new UnityException("Invalid pins");}

		bowls[bowl - 1] = pins;

		if(bowl == 21) {
			return Action.EndGame;
		}

		if(bowl >= 19 && Bowl21Awarded()) {
			bowl++;
			if(pins == 10 || bowls[19 - 1] + bowls[20 - 1] == 10) {
				return Action.Reset;
			}
			return Action.Tidy;
		} else if (bowl == 20 && !Bowl21Awarded()) {
			return Action.EndGame;
		}

		if (pins == 10) { //Strike or Spare of 10
			bowl++;
			if (bowl % 2 == 0) { //Strike
				bowl++;
			}
			return Action.EndTurn;
		}

		if (bowl % 2 != 0 ) {
			bowl += 1;
			return Action.Tidy;
		} else if(bowl % 2 == 0) {
			bowl += 1;
			return Action.EndTurn;
		}

		throw new UnityException("Unsure which Action to return");
	}
	
	private bool Bowl21Awarded() {
		return (bowls[19 - 1] + bowls[20 - 1] >= 10);
	}
}