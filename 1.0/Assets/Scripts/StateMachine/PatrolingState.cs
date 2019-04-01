using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolingState : StateMachineBehaviour {

	//Waypoint gameobjects
	private GameObject[] Waypoints;

	//Current waypoint
	public int curWaypoint;

	private float waypointClearance = 1;

	//Patroling speed
	public float speed;

	//Position of the player
	private Transform playerPosition;


	//Distance that enemy will start attacking player
	public float attackDistance = 15.0f;

	//Distance between enemy and player
	private float distanceToPlayer;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (animator.tag == "EnemyAI_Type1") {
			Waypoints = GameObject.FindGameObjectsWithTag ("Waypoints1");
		} else if (animator.tag == "EnemyAI_Type2") {
			Waypoints = GameObject.FindGameObjectsWithTag ("Waypoints2");
		} else if (animator.tag == "EnemyAI_Type3") {
			Waypoints = GameObject.FindGameObjectsWithTag ("Waypoints3");
		}
		//Find position of the player
		playerPosition = GameObject.FindGameObjectWithTag ("Player").transform;

		//Resume NavmeshAgent
		animator.GetComponentInParent<NavMeshAgent> ().isStopped = false;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {


		InformationUI InfoUI = animator.gameObject.GetComponentInChildren<InformationUI> ();
		AIControls AIControls = GameObject.Find("Canvas").GetComponent<AIControls> ();

		if (AIControls.viewAI) {
			InfoUI.stateLabel.text = "Patroling";
		} else {
			InfoUI.stateLabel.text = "";
		}

		//If waypoint distance is less than waypoint clearance
		if (Vector3.Distance (Waypoints [curWaypoint].transform.position, animator.transform.parent.position) < waypointClearance) {

			//Increase current waypoint by one
			curWaypoint++;

			//if current waypoint is greater than or equal to length of waypoint array
			if (curWaypoint >= Waypoints.Length) {
				//then make current waypoint equal to 0
				curWaypoint = 0;
			}
		}

		//Rotate and look at waypoint
		animator.transform.parent.LookAt(Waypoints[curWaypoint].transform.position);
		if (animator.GetComponentInParent<EnemyAI> ().dead == false) {
			//Move towards waypoint
			animator.GetComponentInParent<NavMeshAgent> ().SetDestination (Waypoints [curWaypoint].transform.position);
		} else {
			//Stop Navmeshagent
			animator.GetComponentInParent<NavMeshAgent> ().enabled = false;
		}
			
		//Check distance to player
		distanceToPlayer = Vector3.Distance(animator.transform.parent.position, playerPosition.position);

		//Detect player is close by
		if(distanceToPlayer < attackDistance)
		{
			if (AIControls.isDetectable) {
				//Stop patrolling and start chasing
				animator.SetBool ("Patroling", false);
				animator.SetBool ("Chasing", true);
			}

		}

		//If patroling turned off
		if (!AIControls.isPatroling) {

			//Go to idle state
			animator.SetBool ("Patroling", false);
			animator.SetBool ("Chasing", false);
			animator.SetBool ("Idle", true);
		}
	}
}
