using UnityEngine;
using System.Collections;

public class RabbitBabyController : MonoBehaviour {

	public bool CharacterActive = false;
	private bool firstMove = true;

	public float speed = 5f;
	private float walkSpeed = 5f;
	//public float climbSpeed = 10f;
	private float sprintSpeed;

	RabbitHoles rabbitHoles;

	private Rigidbody2D rb;
	private SpriteRenderer rend;

	private bool run;

	public float jumpHeight = 7f;
	private bool jump = true;
	private float jumptimer;

	public bool holeEnterAllowed = false;
	public bool holeEntering = false;
	public bool digAllowed = false;
	public bool digging = false;

	//public bool pickupAvailable = false;
	//public GameObject pickUpObject;
	//public bool canDropObject = false;


	private Animator anim;

	//player is facing right instead of left.
	private bool facingRight;



	private void Start()
	{
		rabbitHoles = GameObject.Find ("RabbitHoles").gameObject.transform.GetComponent<RabbitHoles> ();

		rb = GetComponent<Rigidbody2D>();
		rend = GetComponent<SpriteRenderer>();
		run = false;
		anim = GetComponent<Animator> ();
		facingRight = false;
		walkSpeed = speed;
		sprintSpeed = speed * 1.8f;
	}

	public void SetActive() {

	}

	private void Update() {

		if (Input.GetKeyUp (KeyCode.LeftControl)) {
			digging = false;
		}

		if (CharacterActive) {

			if (digAllowed) {

				if (Input.GetKey (KeyCode.LeftShift) && (Input.GetKey (KeyCode.S))) {

					digging = true;


				} else {

					digging = false;
				}
			}

		} 

	}


	private void FixedUpdate()
	{

		// Basic Movement Player //
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (CharacterActive) {


			// HoleEnter /////////////////////////
			if (digAllowed) {
				
				// dig down //
				if (Input.GetKey (KeyCode.LeftControl) && (Input.GetKey (KeyCode.S))) {

					digging = true;

					Debug.Log ("HERE1");


				} else {

					digging = false;
				}

			}



			// HoleEnter /////////////////////////
			if (holeEnterAllowed) {
				if (Input.GetKey (KeyCode.W)) {
					holeEntering = true;
					this.transform.position = rabbitHoles.EnterRabbitHole (this.transform);
					//anim.SetBool ("CubWalk", false);
					//anim.SetBool ("CubClimb", true);
				}
			}
			if (!(holeEnterAllowed)) {
				//anim.SetBool ("CubWalk", false);
				holeEntering = false;
				//anim.SetBool ("CubClimb", false);
			}
			if (holeEnterAllowed) {
				transform.Translate (new Vector3 (0, Time.deltaTime * (speed / 2) * moveVertical, 0));
			} 
			//////////////////////////////////


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


			// Running ////////////////////
			if (Input.GetKey (KeyCode.LeftShift)) {
				if (speed <= walkSpeed * 2) {
					speed = sprintSpeed;
				}
			} else {
				speed = walkSpeed;
			}
			///////////////////////////////	
		}


		if (CharacterActive) {
			// Walking ///////////////////
			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A)) {

				anim.SetBool ("CubWalk", true);

				//Sets x and y basic movement
				transform.Translate (new Vector3 (Time.deltaTime * speed * moveHorizontal, 0, 0));
			} else {
			//	firstMove = true;
				anim.SetBool ("CubWalk", false);
			}
			///////////////////////////////	


			// Turning around ////////////
			turn (moveHorizontal);
			///////////////////////////////	
		} else {

			if (firstMove) {
				StartCoroutine (Wait (1.0f));
			} else {

			// Walking ///////////////////
			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A)) {

				anim.SetBool ("CubWalk", true);

				//Sets x and y basic movement
				transform.Translate (new Vector3 (Time.deltaTime * speed * moveHorizontal, 0, 0));
				
				} else {
					anim.SetBool ("CubWalk", false);
				}

			// Turning around ////////////
				turn (moveHorizontal);
				///////////////////////////////	
			} 
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
			
			if (firstMove) {
				StartCoroutine (Wait (1.0f));
			} else {

				if (moveHorizontal < 0 && !facingRight || moveHorizontal > 0 && facingRight) {

					rend.flipX = !rend.flipX;
					facingRight = !facingRight;
				} 
			}
		}
	}
		

	IEnumerator Wait(float secs) {
		yield return new WaitForSeconds (secs);
		firstMove = false;
	}

}
