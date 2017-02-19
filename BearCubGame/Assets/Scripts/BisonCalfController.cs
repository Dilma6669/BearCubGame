using UnityEngine;
using System.Collections;

public class BisonCalfController : MonoBehaviour {

	public bool CharacterActive = false;
	private bool firstMove = true;


	public float speed = 5f;
	private float walkSpeed = 5f;
	private float sprintSpeed;

	private Rigidbody2D rb;
	private SpriteRenderer rend;

	private bool run;

	public float jumpHeight = 7f;
	private bool jump = true;
	private float jumptimer;

	public bool pushAllowed = false;
	public bool pushing = false;
	public float pushRate;

	private Transform treeObject;
	private Vector3 tempVect3;

	private Animator anim;

	//player is facing right instead of left.
	private bool facingRight;



	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rend = GetComponent<SpriteRenderer>();
		run = false;
		anim = GetComponent<Animator> ();
		facingRight = false;
		walkSpeed = speed;
		sprintSpeed = speed * 1.8f;
	}

	private void Update()
	{
		if (Input.GetKeyUp (KeyCode.LeftControl)) {
			pushing = false;
		}
	}



	private void FixedUpdate()
	{

		// Basic Movement Player //
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (CharacterActive) {


			// Jumping ////////////////////////
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (jump == true) {
					//	anim.SetBool ("Jumping", true);
					rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
					jump = false;
				}
			}
			if (jump == false) {
				jumptimer = jumptimer + 1;
				if (jumptimer >= 50) {
					jumptimer = 0;
					//	anim.SetBool ("Jumping", false);
					jump = true;
				}
			} 

			// Pushing ////////////////////////
			if (pushAllowed) {
				if (Input.GetKey (KeyCode.LeftControl) && (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A))) {
					pushing = true;
					RotateTree ();
				} else {

					pushing = false;
				}
			}
			///////////////////////////////	

			// Running ////////////////////
			if (Input.GetKey (KeyCode.LeftShift)) {
				if (speed <= walkSpeed * 2) {
					speed = sprintSpeed;
				}
			} else {
				speed = walkSpeed;
			}
			///////////////////////////////	



			// Walking ///////////////////
			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A)) {

				anim.SetBool ("CubWalk", true);

				if (!pushing) {
					//Sets x and y basic movement
					transform.Translate (new Vector3 (Time.deltaTime * speed * moveHorizontal, 0, 0));
				}
			} else {
				firstMove = true;
				anim.SetBool ("CubWalk", false);
			}
			///////////////////////////////	


			// Turning around ////////////
			turn (moveHorizontal);
			///////////////////////////////	
		} else {
			/*
			if (firstMove) {
				StartCoroutine (Wait (1.0f));
			} else {

				// Walking ///////////////////
				if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A)) {

					anim.SetBool ("CubWalk", true);

					if (!pushing) {
						//Sets x and y basic movement
						transform.Translate (new Vector3 (Time.deltaTime * speed * moveHorizontal, 0, 0));
					}

				} else {
					anim.SetBool ("CubWalk", false);
				}

				// Turning around ////////////
				turn (moveHorizontal);
				///////////////////////////////	
			} */
		}
	}


	//code 	for turning the player to either right or left.
	private void turn(float moveHorizontal)
	{
		if (CharacterActive) {


			if (moveHorizontal < 0 && !facingRight || moveHorizontal > 0 && facingRight) {

				rend.flipX = !rend.flipX;
				facingRight = !facingRight;
			} 
		} else {
			/*
			if (firstMove) {
				StartCoroutine (Wait (1.0f));
			} else {

				if (moveHorizontal < 0 && !facingRight || moveHorizontal > 0 && facingRight) {

					rend.flipX = !rend.flipX;
					facingRight = !facingRight;
				} 
			} */
		}
	}

	private void RotateTree() {


		if (facingRight) {

			tempVect3.x = 0;
			tempVect3.y = 0;
			tempVect3.z = -pushRate;

		} else {
			
			tempVect3.x = 0;
			tempVect3.y = 0;
			tempVect3.z = pushRate;
		}

		treeObject.Rotate(tempVect3);
		//pushAllowed = false;
	}


	public void SetTree(GameObject tree) {

		treeObject = tree.transform;
		tempVect3 = treeObject.position;

	}


	IEnumerator Wait(float secs) {
		yield return new WaitForSeconds (secs);
		firstMove = false;
	}

}