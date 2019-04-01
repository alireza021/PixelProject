using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogAI : MonoBehaviour {

	//Position of player
	public GameObject player;

	//Distance to stay with player
	public float closeDistance = 2.5F;

	//Dog's follow speed
	public float speed = 5.0F;

	//Check if should be following
	public bool isFollowing;

	//Array of all enemies
	private GameObject[] targets;

	//Dog's animator
	private Animator anim;

	//Closest enemies position
	private Transform closestEnemy;

	//Check if attacking
	public bool isAttacking;

	//Distance to start attacking
	public float attackDistance = 25f;

	//Distance to kill enemy
	public float killDistance = 2F;

	//Distance between enemy and dog
	private float distanceToEnemy;

	//Dog attack ray
	public Ray biteRay = new Ray();

	//Dog attack raycast hit
	public RaycastHit biteHit;

	//Detect enemies that should be attacked
	int biteableMask;

	void Start () {

		//Find player
		player = GameObject.FindGameObjectWithTag("Player");

		//Assign animator
		anim = GetComponentInChildren<Animator> ();
	}

	void Update() {
		
		biteableMask = LayerMask.GetMask ("Shootable");

		//Find all enemies
		targets = GameObject.FindGameObjectsWithTag("Blood");

		AIControls AIControls = GameObject.Find("Canvas").GetComponent<AIControls> ();

		//Get square magnitute of distance to player
		Vector3 offsetPlayer = player.transform.position - transform.position;
		float sqrLen = offsetPlayer.sqrMagnitude;

		//If is following
		if (player){
			if (isFollowing) {

				//If close to player
				if (sqrLen < closeDistance * closeDistance) {
					//Smoothly rotate towards whatever the player is looking at
					transform.rotation = Quaternion.Slerp (transform.rotation, player.transform.rotation, .05f);
				} else {
					//Look at player and move forward
					transform.LookAt (player.transform);
					transform.Translate (Vector3.forward * speed * Time.deltaTime);
				}
			}
			//If got too far
			if (sqrLen > 1000f) {
				
				//Get back to player position 
				transform.position = player.transform.position;
			}
		}

		//Dog's vision distance limit
		float visionLimit = 30f;
		GameObject closest = null;

		//For each enemy in the game
		foreach (GameObject target in targets) {

			//If any enemies exist
			if (target != null) {

				//Distance difference to enemies
				Vector3 distanceOffset = target.transform.position - transform.position;

				//Get square magnitute of distance difference
				float curDistance = distanceOffset.sqrMagnitude;

				//If distance is less than vision limit
				if (curDistance < visionLimit) {

					//Identify closest target
					closest = target;

					//Set vision limit to current distance
					visionLimit = curDistance;

					//Find position of closest enemy
					closestEnemy = closest.transform;
				}
			}
		}
			

		//If enemy is within vision limit
		if (AIControls.dogAttacking) {
			if (closestEnemy) {

				//Check distance to enemy
				distanceToEnemy = Vector3.Distance (transform.position, closestEnemy.transform.position);

				//If distance to enemy is less than or equal to attack distance
				if (distanceToEnemy <= attackDistance) {

					//Stop following player
					isFollowing = false;

					//Get distance difference to closest enemy
					Vector3 offset = closestEnemy.position - transform.position;

					//Get square magnitute of distance difference
					float sqrMag = offset.sqrMagnitude;

					//If kill distance is greater than difference
					if (sqrMag < killDistance * killDistance) {

						//First look at the enemy
						transform.LookAt (closestEnemy);

						//Turn on attack animation
						anim.SetBool ("attack", true);

						//Call enemies script
						EnemyAI enemyStatus = closestEnemy.GetComponent<Collider> ().GetComponent<EnemyAI> ();

						//If enemy is not destroyed already
						if (enemyStatus != null) {

							//Send ray from dog's position
							biteRay.origin = transform.position;

							//Send ray forward
							biteRay.direction = transform.forward;

							//If ray hits attackable object
							if (Physics.Raycast (biteRay, out biteHit, 10f, biteableMask)) {

								//Kill Enemy
								Rigidbody enemyBody = biteHit.collider.GetComponent<Rigidbody> ();
								enemyBody.isKinematic = false;
								enemyStatus.TakeDamage (closestEnemy.transform.position);
								enemyStatus.ApplyForce (enemyBody, biteHit);
							}
						}

					//If not within Kill distance
					} else {

						//Turn of attack animation
						anim.SetBool ("attack", false);

						//Move forward towards the enemy until reach kill distance
						transform.Translate (Vector3.forward * 8 * Time.deltaTime);
						transform.LookAt (closestEnemy);
					}
				}

				//If enemy is not within vision limit
			} else {

				//Turn off attack animation
				anim.SetBool ("attack", false);

				//Start following player
				isFollowing = true;
			}
		}
		
			
		//If left shift is pressed
		if (Input.GetKey (KeyCode.LeftShift)) {

			//Increase dog's speed
			speed = 9;
		} else {
			speed = 5;
		}
			
	}
}
