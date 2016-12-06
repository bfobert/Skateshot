using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FPSInput : MonoBehaviour {
	private float maxSpeed = 11f;
	private float accelSpeed = .25f;
	private float decelSpeed = .15f;
	private float deltaX = 0f;
	private float deltaZ = 0f;
	[SerializeField] float accelerationX = 0f;
	[SerializeField] float accelerationZ = 0f;
	private float combinedAcceration = 0f;
	//	[SerializeField] float horizSpeed = 9f;
	private float gravity = -.4f;
	private float vertSpeed = 0f;
	private float jumpSpeed = 10f;
//	private float currRotation = 0f;

	[SerializeField] private int playerNum;
	Player player;

	private CharacterController charController;

	void Start() {
		charController = GetComponent<CharacterController>();
//		currRotation = charController.transform.rotation - 90;
		player = (Player)gameObject.GetComponent<Player>();
	}

	void Update() {
		
		//for "present" map
		//Written by Patrick and Mark
		if (player.GetTimeStatus() == false) {
			gravity = -.225f;
		} else {
			gravity = -.4f;
		}

		//Jumps if the player presses the jump key
		if (charController.isGrounded) {
			//We want to keep this down, but it causes sticky jumps 
			//vertSpeed = 0;
			if (Input.GetButtonDown ("Joy" + playerNum + "_Jump")) {
				vertSpeed = jumpSpeed;
			}

		}

		//Limit the acceleration
		combinedAcceration = Mathf.Abs(accelerationX)*Mathf.Abs(accelerationX) + Mathf.Abs(accelerationZ)*Mathf.Abs(accelerationX);
		combinedAcceration = Mathf.Sqrt (combinedAcceration);
		if (Mathf.Abs(accelerationX) >2f && Mathf.Abs(accelerationZ) >2f) {
			accelerationX = Mathf.Clamp (accelerationX, -maxSpeed + 2, maxSpeed - 2);
			accelerationZ = Mathf.Clamp (accelerationZ, -maxSpeed + 2, maxSpeed - 2);
		} else {
			accelerationX = Mathf.Clamp (accelerationX, -maxSpeed, maxSpeed);
			accelerationZ = Mathf.Clamp (accelerationZ, -maxSpeed, maxSpeed);
		}





		//Most of the acceleration and sliding mechanics written by Ben, some testing done by Mark and Patrick

		//Moving in the x+ Direction (right)
		if (Input.GetAxis ("Joy" + playerNum + "_LeftStickHorizontal") == 1) {
			//Increase velocity in positive x direction. Multiply to make it "zippy"
			accelerationX  += accelSpeed*2;
			deltaX = accelerationX;
		}

		//		//Moving in the x- Direction (left)
		if (Input.GetAxis ("Joy" + playerNum + "_LeftStickHorizontal") == -1) {
			//Increase velocity in negative x direction. Multiply to make it "zippy"
			accelerationX -= accelSpeed*2;
			deltaX = accelerationX;
		}

		//Moving in the z+ Direction (fowards)
		if (Input.GetAxis ("Joy" + playerNum + "_LeftStickVertical") == 1) {
			//Increase velocity in positive z direction
			accelerationZ += accelSpeed;
			deltaZ = accelerationZ;
		}


		//Moving in the z- Direction (backwards)
		if (Input.GetAxis ("Joy" + playerNum + "_LeftStickVertical") == -1) {
			//Increase velocity in positive z direction
			accelerationZ -= accelSpeed;
			deltaZ = accelerationZ;
		}


		//If player is not moving left or right and acceleration is greater than 1...sliding in x direction
		if (Input.GetAxis ("Joy" + playerNum + "_LeftStickHorizontal") == 0 && Mathf.Abs (accelerationX) > 1f) {
			//if going left, acceleration is negative so add the decel speed
			if (accelerationX < 0) {
				accelerationX += decelSpeed;
				deltaX = accelerationX;
			}
			//if going right...
			else {
				accelerationX -= decelSpeed;
				deltaX = accelerationX;
			}
		}
		//if left-right acceleration is less than 2 and the player is not touching the stick
		if(Input.GetAxis("Joy" + playerNum + "_LeftStickHorizontal") == 0 && Mathf.Abs (accelerationX) < 2f){
			//stop in x direction
			deltaX = 0f;
		}



		//If player is not moving forward or back and acceleration is greater than 1...sliding in z direction
		if (Input.GetAxis ("Joy" + playerNum + "_LeftStickVertical") == 0 && Mathf.Abs (accelerationZ) > 1f) {
			//if going backwards, acceleration is negative so add the decel speed
			if (accelerationZ < 0) {
				accelerationZ += decelSpeed;
				deltaZ = accelerationZ;
			}
			//if going forwards...
			else {
				accelerationZ -= decelSpeed;
				deltaZ = accelerationZ;
			}
		}
		//if front-back acceleration is less than 2 and player is not touching the stick
		if(Input.GetAxis("Joy" + playerNum + "_LeftStickVertical") == 0 && Mathf.Abs (accelerationZ) < 2f){
			//stop in Z direction
			deltaZ = 0f;
		}

		//Here is where we are calling the move code
		Vector3 movement = new Vector3 (deltaX, vertSpeed, deltaZ);
		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		charController.Move (movement);


		//Causes "gravity" when jumping
		if(!charController.isGrounded){
			if (vertSpeed > -26f) {
				vertSpeed += gravity;
			}
		}
	}

}
