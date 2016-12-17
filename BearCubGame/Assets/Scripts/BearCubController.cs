using UnityEngine;
using System.Collections;

public class BearCubController : MonoBehaviour {   

	public bool CharacterActive = true;
	private bool firstMove = true;


	public float speed = 5f;
	private float walkSpeed = 5f;
	public float climbSpeed = 10f;
	private float sprintSpeed;

	private Rigidbody2D rb;
	private SpriteRenderer rend;

	private bool run;

	public float jumpHeight = 7f;
	private bool jump = true;
	private float jumptimer;

	public bool climbAllowed = false;
	public bool cubClimbing = false;

	public bool pickupAvailable = false;
	public GameObject pickUpObject;
	public bool canDropObject = false;


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
		if (CharacterActive) {
			
			if (canDropObject) {

				if (Input.GetKeyDown (KeyCode.LeftControl)) {

					pickUpObject.transform.GetComponent<PolygonCollider2D> ().enabled = true;
					pickUpObject.transform.SetParent (this.transform);
					pickUpObject.transform.GetComponent<Rigidbody2D> ().isKinematic = false;
					pickUpObject.transform.SetParent (GameObject.Find ("StoneContainer").gameObject.transform);

					Vector2 vect = this.transform.position;

					if (facingRight) {

						vect.x += -1.0f;
					} else {
						vect.x += 1.0f;
					}

					pickUpObject.transform.position = vect;
					pickupAvailable = true;
					canDropObject = false;
				}

			} else {

				if (pickupAvailable) {

					if (Input.GetKeyDown (KeyCode.LeftControl)) {

						pickUpObject.transform.GetComponent<PolygonCollider2D> ().enabled = false;
						pickUpObject.transform.SetParent (this.transform);
						pickUpObject.transform.GetComponent<Rigidbody2D> ().isKinematic = true;

						pickUpObject.transform.position = this.transform.position;
						pickupAvailable = false;
						canDropObject = true;
					}

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
			// Climbing /////////////////////////
			if (climbAllowed) {
				if (Input.GetKey (KeyCode.W)) {
					cubClimbing = true;
					anim.SetBool ("CubWalk", false);
					anim.SetBool ("CubClimb", true);
					this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
				}
			}
			if (!(climbAllowed)) {
				anim.SetBool ("CubWalk", false);
				cubClimbing = false;
				anim.SetBool ("CubClimb", false);
				this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
			}
			if (climbAllowed) {
				transform.Translate (new Vector3 (0, Time.deltaTime * (speed / 2) * moveVertical, 0));
			}
			//////////////////////////////////


			// Jumping ////////////////////////
			if (!cubClimbing) {
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

				if (!cubClimbing) {
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

			if (firstMove) {
				StartCoroutine (Wait (1.0f));
			} else {

				// Walking ///////////////////
				if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A)) {

					anim.SetBool ("CubWalk", true);

					if (!cubClimbing) {
						//Sets x and y basic movement
						transform.Translate (new Vector3 (Time.deltaTime * speed * moveHorizontal, 0, 0));
					}

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