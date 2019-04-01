using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void StartGameBtn(string newGame) {
	
		SceneManager.LoadScene ("Game");
	
	}
	public void MainMenuBtn(string mainMenu) {

		SceneManager.LoadScene ("Menu");

		Time.timeScale = 1;
		AudioListener.pause = false;
		GameObject.Find ("Player").GetComponentInChildren<PlayerShooting> ().enabled = true;

	}
	public void Restart() {

		SceneManager.LoadScene ("Game");
		Time.timeScale = 1;
		AudioListener.pause = false;
		GameObject.Find ("Player").GetComponentInChildren<PlayerShooting> ().enabled = true;

	}

	public void Quit() {

		Application.Quit ();

	}
}
