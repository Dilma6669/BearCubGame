using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RabbitBabyController : MonoBehaviour {

	public bool CharacterActive = false;
	private bool firstMove = true;

	private float currentSpeed;
	public float walkSpeed = 1f;
	public float digSpeed = 1f;
	private float sprintSpeed = 1f;
	public float jumpHeight = 7f;

	RabbitHoles rabbitHoles;
	DirtController dirtControllerScript;

	private Rigidbody2D rb;
	private SpriteRenderer rend;

	public bool jumpAllowed = true;
	private float jumptimer;

	public bool holeEnterAllowed = false;
	public bool holeEntering = false;
	public bool digAllowed = false;
	public bool digging = false;
	public bool rabbitRunnning = false;

	public List<Transform> dirtTransList;

	private Animator anim;

	//player is facing right instead of left.
	private bool facingRight;



	private void Start()
	{
		rabbitHoles = GameObject.Find ("RabbitHoles").gameObject.transform.GetComponent<RabbitHoles> ();
		dirtControllerScript = GameObject.Find ("DirtController").gameObject.transform.GetComponent<DirtController> ();

		rb = GetComponent<Rigidbody2D>();
		rend = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator> ();
		facingRight = false;
		currentSpeed = walkSpeed;
	}

	public void SetActive() {

	}

	private void Update() {


	}



	private void FixedUpdate()
	{

		if (CharacterActive) {

			// Basic Movement Player //
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			// Jumping ////////////////////////
			if (!digging) {
				if (jumpAllowed == true) {
					if (Input.GetKeyDown (KeyCode.Space)) {
						//	anim.SetBool ("Jumping", true);
						rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
						jumpAllowed = false;
					}
				} 
			}
			//////////////////////////////////



			// Digging Hole /////////////////////////
			if (Input.GetKey (KeyCode.S)) {
				anim.SetBool ("RabbitDig", true);
				rabbitRunnning = false;
				digging = true;
				if (digAllowed) {
					dirtControllerScript.DeleteDirtList ();
				}
			} else if (Input.GetKeyUp (KeyCode.S)) {
				digging = false;
				anim.SetBool ("RabbitDig", false);
			}
			//////////////////////////////////



			// HoleEnter /////////////////////////
			if (holeEnterAllowed) {
				if (Input.GetKey (KeyCode.W)) {
					holeEntering = true;
					this.transform.position = rabbitHoles.EnterRabbitHole (this.transform);
					transform.Translate (new Vector3 (0, Time.deltaTime * currentSpeed * moveVertical, 0));
				}
			} else if (!holeEnterAllowed) {
				anim.SetBool ("RabbitWalk", false);
				holeEntering = false;
			}
			//////////////////////////////////



			// Running ////////////////////
			if (Input.GetKey (KeyCode.LeftShift)) {
				rabbitRunnning = true;
				//anim.SetBool ("RabbitRun", true);
				if (currentSpeed <= walkSpeed * 2) {
					currentSpeed = walkSpeed * 2;
				}
			} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
				rabbitRunnning = false;
				currentSpeed = walkSpeed;
			}
			///////////////////////////////	


			// Walking ///////////////////
			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A)) {
				transform.Translate (new Vector3 (Time.deltaTime * currentSpeed * moveHorizontal, 0, 0));
				if (digging) {
					currentSpeed = digSpeed;
				} else if (!digging) {
					currentSpeed = walkSpeed;
					anim.SetBool ("RabbitWalk", true);
				}
			} else if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.A)) {
				anim.SetBool ("RabbitWalk", false);
			}


			// Turning around ////////////
			turn (moveHorizontal);
			///////////////////////////////
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
		}
	}
		


	IEnumerator Wait(float secs) {
		yield return new WaitForSeconds (secs);
		firstMove = false;
	}

}
