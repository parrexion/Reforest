using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan {

	public static void MainMenu() {
		SceneManager.LoadScene(0);
	}

	public static void IntroScene() {
		SceneManager.LoadScene(1);
	}

	public static void NewGame() {
		SceneManager.LoadScene(2);
	}
	
	public static void GameOver() {
		SceneManager.LoadScene(3);
	}

	public static void QuitGame() {
		Application.Quit();
	}
}
