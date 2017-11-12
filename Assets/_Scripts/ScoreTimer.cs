using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTimer : MonoBehaviour {

	public Text scoreLabel;
	public float dayLength = 20;
	private float currentScore;

	// Use this for initialization
	void Start () {
		StartCoroutine(updateScore());
	}

	IEnumerator updateScore() {
		currentScore++;
		scoreLabel.text = "Current day: " + (int)(currentScore);
		yield return new WaitForSeconds(dayLength);
	}
}
