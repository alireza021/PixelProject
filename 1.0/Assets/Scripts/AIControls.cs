using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControls : MonoBehaviour
{

	public bool isDetectable;

	public bool isPatroling;

	public bool dogAttacking;

	public bool viewAI;



	void Start(){
		isDetectable = true;
		isPatroling = true;
		dogAttacking = false;
		viewAI = false;

	}
    
	void Update(){

	}

	public void detectableToggle(bool detectable){

		isDetectable = detectable;
	}

	public void patrolingToggle(bool patrol){
		isPatroling = patrol;
	}
		

	public void dogToggle(bool dog){
		dogAttacking = dog;
	}

	public void infoToggle(bool info){
		viewAI = info;
	}
}
