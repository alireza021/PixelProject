using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		PlayerMotor checker = GameObject.Find ("Player").GetComponent<PlayerMotor> ();
		HealthBar checker2 = GameObject.Find ("Player").GetComponent<HealthBar> ();
	
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D)) {

			anim.SetBool ("walk", true);
		} else {
			anim.SetBool("walk", false);
		}

		if (checker.running == false) {
			anim.SetBool ("run", false);
		}
		if (checker.running == true) {
			anim.SetBool ("run", true);
		}
		if (checker2.currentHealth <= 0) {
			anim.SetBool ("dead", true);
		
		}
			
	}
}
