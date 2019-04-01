using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponentInParent<NavMeshAgent> ().isStopped = true;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
		AIControls AIControls = GameObject.Find("Canvas").GetComponent<AIControls> ();

		//If patroling turned on
		if (AIControls.isPatroling) {

			//Start patroling state
			animator.SetBool ("Idle", false);
			animator.SetBool ("Chasing", false);
			animator.SetBool ("Patroling", true);
		//Else display idle state
		} else {
			InformationUI InfoUI = animator.gameObject.GetComponentInChildren<InformationUI> ();
			if (AIControls.viewAI) {
				InfoUI.stateLabel.text = "Idle";
			} else {
				InfoUI.stateLabel.text = "";
			}
		}
	}
}
