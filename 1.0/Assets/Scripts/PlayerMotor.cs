using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

	//Player character controller
	private CharacterController playerController;

	//Player speed
	public float speed = 6.0F;

	//Player jump speed
	public float jumpSpeed = 8.0F;

	//Player rotation speed
	public float rotateSpeed = 4.0f;

	//Is player running?
	public bool running;

	//Player move direction vector
	private Vector3 moveDirection = Vector3.zero;

	//Gravity force
	public float gravity = 20.0F;

	void Start () {
		
		playerController = GetComponent<CharacterController> ();
		running = false;
	}
	

	void Update () {

		//If player is not jumping
		if (playerController.isGrounded) {
			
			//Change players horizontal movement to WASD or arrow keys
			moveDirection = new Vector3(Input.GetAxis("Horizontal") , 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);

			//Apply speed variable to movement
			moveDirection *= speed;

			//If jump button is pressed
			if (Input.GetButton("Jump"))

				//Make player jump
				moveDirection.y = jumpSpeed;
		}

		//Change players x-rotation to mouse horizontal keys
		transform.Rotate (0, Input.GetAxis ("Mouse X") * 0.8f, 0);

		//Stick player to the ground unless jumping
		moveDirection.y -= gravity * Time.deltaTime;
		playerController.Move(moveDirection * Time.deltaTime);

		//If left shift is pressed
		if (Input.GetKey (KeyCode.LeftShift)) {

			//Increase players speed
			speed = 9;
			running = true;
		} else {
			
			speed = 5;
			running = false;
		}
	}
}
