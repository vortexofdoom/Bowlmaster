﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreMaster {
	
	//returns a list of cumulative scores, like a normal score card.
	public static List<int> ScoreCumulative(List<int> rolls) {
		List<int> cumulativeScores = new List<int>();
		int runningTotal = 0;
		
		foreach (int frameScore in ScoreFrames(rolls)) {
			runningTotal += frameScore;
			cumulativeScores.Add(runningTotal);
		}

		return cumulativeScores;
	}

	// Return a list of individual frame scores
	public static List<int> ScoreFrames (List<int> rolls) {
		List<int> frames = new List<int>();

		for (int i = 1; i < rolls.Count; i += 2) {
			if(rolls[i-1] + rolls[i] < 10) {	//Normal "open" frame
				frames.Add(rolls[i - 1] + rolls[i]);
			}
			else if(rolls[i - 1] == 10 && i + 1 < rolls.Count) {    //Strike
				frames.Add(10 + rolls[i] + rolls[i + 1]);
				if(frames.Count < 10) { i--; }						//Adjusts index if not on last frame
			} 
			else if (rolls[i-1] + rolls[i] == 10 && i + 1 < rolls.Count) {	//Spare
				frames.Add(10 + rolls[i + 1]);
			}
		}
		return frames;
	}

}