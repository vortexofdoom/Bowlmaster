﻿using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

[TestFixture]
public class ActionMasterTest {

	private List<int> pinFalls;
	private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
	private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
	private ActionMaster.Action reset = ActionMaster.Action.Reset;
	private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

	[SetUp]
	public void Setup() {
		pinFalls = new List<int>();
	}

	[Test]
	public void T01FirstBallStrikeReturnsEndTurn() {
		pinFalls.Add(10);
		Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
	}

	[Test]
	public void T02Bowl8ReturnsTidy() {
		pinFalls.Add(8);
		Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
	}

	[Test]
	public void T03Bowl28ReturnsEndTurn() {
		int[] rolls = { 8, 2 };
		Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
	}

	[Test]
	public void T04CheckResetAtStrikeLastFrame() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 };
		Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
	}

	[Test]
	public void T05CheckResetAtSpareLastFrame() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5 };
		Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
	}

	[Test]
	public void T06YouTubeRollsResultInEndGame() {
		int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2, 9 };
		Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
	}

	[Test]
	public void T07GameEndsAtRoll20() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
		Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
	}

	[Test]
	public void T08Roll20AfterStrikeReturnsTidyUnlessStrike() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 5 };
		Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
	}

	[Test]
	public void T09SpareOf10ReturnsSpare() {
		int[] rolls = { 0, 10, 5, 1 };
		Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
	}

	[Test]
	public void T10Dondi10thFrameTurkey() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10, 10 };
		Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
	}

	[Test]
	public void T11ZeroOneEndsTurn() {
		int[] rolls = { 0, 1 };
		Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
	}
}
