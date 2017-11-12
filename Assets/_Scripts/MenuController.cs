using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	

	public void StartNewGame () {
		SceneMan.IntroScene();
	}

	public void QuitGame() {
		SceneMan.QuitGame();
	}
}
