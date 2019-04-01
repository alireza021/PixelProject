using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HealthBar : MonoBehaviour {

	public Text gameOver;
	public Text highscoreText;
	public Slider healthbar;
	private float damageDistance = 1.0f;
	public float currentHealth;
	public float maxHealth;
	GameObject[] enemies;




	void Start () {

		gameOver.text = "";
		highscoreText.text = "";
		maxHealth = 300.0f;
		currentHealth = maxHealth;
		healthbar.value = CalculateHealth ();

	}
	

	void Update () {
		
		enemies  = GameObject.FindGameObjectsWithTag("Blood");

		foreach (GameObject enemy in enemies) {
			if (enemy != null) {
				float distance = Vector3.Distance (enemy.transform.position, transform.position);

				if (distance < damageDistance && enemy.GetComponent<EnemyAI>().dead == false && Time.timeScale == 1) {
					DealDamage (1);


					healthbar.value = CalculateHealth ();
				}
	
			}
		}
	}

	float CalculateHealth(){
		return currentHealth / maxHealth;

	}
	void DealDamage(float damageValue){
		currentHealth -= damageValue;
		AudioSource zombie = GetComponent<AudioSource> ();
		zombie.Play ();

		if (currentHealth <= 0) {
			Die ();
		}

	}

	public void Die(){
		currentHealth = 0;
		gameObject.GetComponent<CharacterController> ().enabled = false;
		gameObject.GetComponent<PlayerMotor> ().enabled = false;
		gameObject.GetComponentInChildren<PlayerShooting> ().enabled = false;
		gameOver.text = "GAME OVER";
		Cursor.visible = true;
		GameObject.Find("Restart").GetComponent<Button>().enabled = true;
		GameObject.Find("Restart").GetComponent<Image>().enabled = true;
		GameObject.Find("Restart").GetComponentInChildren<Text>().enabled = true;

		GameObject.Find("BackTo").GetComponent<Button>().enabled = true;
		GameObject.Find("BackTo").GetComponent<Image>().enabled = true;
		GameObject.Find("BackTo").GetComponentInChildren<Text>().enabled = true;
		PlayerShooting highscore = GetComponentInChildren<PlayerShooting> ();

		if (PlayerPrefs.GetFloat ("Highscore") < highscore.points) {
			PlayerPrefs.SetFloat ("Highscore", highscore.points);
			highscoreText.text = "NEW HIGHSCORE!";
		}



	}
}


