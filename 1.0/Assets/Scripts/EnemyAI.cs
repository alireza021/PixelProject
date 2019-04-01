using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

	//Boolean to know if dead
	public bool dead = false;

	//Enemy animator reference (State Machine is within animator)
	private Animator enemyAnimator;

	//Blood spatter particle system
	ParticleSystem hitParticles;

	void Start () {

		//Find blood spatter hit particles
		hitParticles = GameObject.Find("hitParticles").GetComponent<ParticleSystem> ();

		//Asign animator
		enemyAnimator = gameObject.GetComponentInChildren<Animator> ();
	}
		
	//Damage taking/dying function
	public void TakeDamage(Vector3 hitPoint){

		//If enemy is not dead
		if (!dead) {
			


			//If hit particles exist
			if (hitParticles != null) {

				//Activate hit particles at hitpoint
				hitParticles.transform.position = hitPoint;

				//Play hit particles
				hitParticles.Play ();
			}

			//Player is dead
			dead = true;

			//Play dead animation
			enemyAnimator.SetBool ("dead", true);


			//Increase killcount
			PlayerShooting pointcount = GameObject.Find("Player").GetComponentInChildren<PlayerShooting>();
			pointcount.points++;

			//Remove dead enemy after 4 seconds
			Invoke ("removeMe", 4.0f);
		}
	}

	//Function to apply force when shot
	public void ApplyForce(Rigidbody body, RaycastHit hit) {
		body.velocity = Vector3.zero;

		//Add force from hitpoint towards up
		body.AddForceAtPosition(hit.transform.up * 300, hit.point);
	}

	//Function to destroy gameobject
	void removeMe (){
		Destroy(gameObject);
	}
}
