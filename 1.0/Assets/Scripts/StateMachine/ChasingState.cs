using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingState : StateMachineBehaviour {

	//Position of the player
	private Transform playerPosition;

	//How fast will zombie reach player?
	public float reachTime = 3.0f;

	//Distance that enemy will stop attacking player
	public float leaveDistance = 20.0f;

	//Distance between enemy and player
	private float distanceToPlayer;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//Find position of the player
		playerPosition = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

			
		InformationUI InfoUI = animator.gameObject.GetComponentInChildren<InformationUI> ();
		AIControls AIControls = GameObject.Find("Canvas").GetComponent<AIControls> ();

		if (AIControls.viewAI) {
			InfoUI.stateLabel.text = "Chasing";
		} else {
			InfoUI.stateLabel.text = "";
		}

		//Look at the player
		animator.transform.parent.LookAt (playerPosition);

		if (animator.GetComponentInParent<EnemyAI> ().dead == false) {
			//Move towards the player by the reachTime variable
			animator.GetComponentInParent<NavMeshAgent> ().SetDestination (playerPosition.position);
		} else {
			//Turn of navmeshagent
			animator.GetComponentInParent<NavMeshAgent> ().enabled = false;
		}

		//Check distance to player
		distanceToPlayer = Vector3.Distance(animator.transform.parent.position, playerPosition.position);

		//Detect player is far away
		if(distanceToPlayer > leaveDistance)
		{
			//Stop chasing and start patroling
			animator.SetBool ("Chasing", false);
			animator.SetBool ("Idle", false);
			animator.SetBool ("Patroling", true);
		}

		//If not detectable
		if (!AIControls.isDetectable) {
			//Start patroling
			animator.SetBool ("Chasing", false);
			animator.SetBool ("Idle", false);
			animator.SetBool ("Patroling", true);
		}
	}
}
