using UnityEngine;
using System.Collections;



public class FirstPersonController : MonoBehaviour {


	// alters the speed of player movement
	public float movementSpeed;

	// alters the speed in which the player rotates 
	public float mouseSensitivity;

	// controls the maximum angle the player can pitch
	public float maximumPitchAngle;
	private float pitchRotation = 0.0f;

	// Handles gravity and jumping
	private float verticalSpeed = 0.0f;
	public float jumpSpeed = 20f;
	
	// as the variable suggests
	public float movingWhileJumpingSpeed;
	
	// reference to character controller
	CharacterController cc;

	// reference to idle frame (box collider when at rest)
	//GameObject idleCapsule;

	// Use this for initialization
	void Start () {


		cc = GetComponent<CharacterController> ();

		//idleCapsule = GameObject.FindGameObjectsWithTag ("Idle Frame")[0];
	}
	
	// Update is called once per frame
	void Update () {

		HandleRotation ();
		HandleJumping ();
		HandleCrouching ();
		HandleMovement ();
	
	}

	void HandleRotation() {
	
		// get mouse movement for player rotation
		float jawRotation = Input.GetAxis ("Mouse X") * mouseSensitivity;
		pitchRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		
		// limit pitch rotation between desired angles and locally rotate trough Euler angles
		pitchRotation = Mathf.Clamp (pitchRotation, -maximumPitchAngle, maximumPitchAngle);
		Camera.main.transform.localRotation = Quaternion.Euler (pitchRotation, 0, 0);

		//Camera.main.transform.Rotate (-pitchRotation, 0.0f, 0.0f);
		transform.Rotate (0,jawRotation,0);
	}

	void HandleCrouching() {

		// Crouch down if left ctrl is pressed down
		if (Input.GetButtonDown ("Crouch") ) {

			transform.localScale = new Vector3 (1.0f, 0.5f, 1.0f);
			movementSpeed = movementSpeed/2f;
		}
		if (Input.GetButtonUp ("Crouch") ) {

			transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			movementSpeed = movementSpeed * 2f;
		}
	}


	void HandleMovement() {

		// get user input
		float forwardSpeed = Input.GetAxis ("Vertical");
		float sideSpeed = Input.GetAxis ("Horizontal");
		
		// create input vector
		Vector3 movement = new Vector3 (sideSpeed * movementSpeed, verticalSpeed ,forwardSpeed * movementSpeed);
		
		// take into account that player can be turning
		movement = transform.rotation * movement;
		
		// move player
		cc.Move (movement * Time.deltaTime);
	}


	/*
	 *	sets jumpSpeed if player is jumping
	 * 	sets to gravity if player is not jumping
	 * 	sets to accelerated gravity when in air
	 */
	void HandleJumping() {

		if (cc.isGrounded) {

			if (Input.GetButtonDown ("Jump")) {verticalSpeed = jumpSpeed;} 
			else {verticalSpeed = Physics.gravity.y;}
		} 
		else {
	
		verticalSpeed += Physics.gravity.y * Time.deltaTime;
		}
	}
	
}
