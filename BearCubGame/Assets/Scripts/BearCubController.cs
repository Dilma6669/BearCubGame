using UnityEngine;
using System.Collections;

public class BearCubController : MonoBehaviour {   

	public bool CharacterActive = true;
	private bool firstMove = true;


	private float currentSpeed;
	public float walkSpeed = 1f;
	public float climbSpeed = 1f;
	public float jumpHeight = 1f;

	private Rigidbody2D rb;
	private SpriteRenderer rend;

	public bool jumpAllowed = true;
	private float jumptimer;

	public bool climbAllowed = false;
	public bool cubClimbing = false;
	public bool cubRunnning = false;

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
		anim = GetComponent<Animator> ();
		facingRight = false;
		currentSpeed = walkSpeed;
	}
		
	private void Update()
	{
		if (CharacterActive) {
			
			if (canDropObject) {

				if (Input.GetKeyDown (KeyCode.LeftControl)) {

					pickUpObject.transform.GetComponent<PolygonCollider2D> ().enabled = true;
					//pickUpObject.transform.SetParent (this.transform);
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

			} else if (pickupAvailable) {

				if (Input.GetKeyDown (KeyCode.LeftControl)) {

					pickUpObject.transform.GetComponent<PolygonCollider2D> ().enabled = false;
					pickUpObject.transform.SetParent (this.transform);
					pickUpObject.transform.GetComponent<Rigidbody2D> ().isKinematic = true;
					pickUpObject.transform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);

					pickUpObject.transform.position = this.transform.position;
					pickUpObject.transform.rotation = this.transform.rotation;
					pickupAvailable = false;
					canDropObject = true;
				}

			}
		}
	}


	private void FixedUpdate()
	{

		if (CharacterActive) {

		// Basic Movement Player //
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

			// Climbing /////////////////////////
			if (cubClimbing) {
				if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) {
					transform.Translate (new Vector3 (0, Time.deltaTime * currentSpeed * moveVertical, 0));
				}

				if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A)) {
					transform.Translate (new Vector3 (Time.deltaTime * currentSpeed * moveHorizontal, 0, 0));
				}

			} else if (climbAllowed) {
				if (Input.GetKey (KeyCode.W)) {
					cubClimbing = true;
					anim.SetBool ("CubClimb", true);
					anim.SetBool ("CubWalk", false);
					//	anim.SetBool ("Jumping", false);
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
					GetComponent<Rigidbody2D> ().gravityScale = 0;
					currentSpeed = climbSpeed;
				}
			} else if (!climbAllowed) {
					cubClimbing = false;
					anim.SetBool ("CubClimb", false);
					this.gameObject.GetComponent<Rigidbody2D> ().gravityScale = 1.5f;
				}
			//////////////////////////////////


			// Jumping ////////////////////////
			if (!cubClimbing) {
				if (Input.GetKeyDown (KeyCode.Space) && jumpAllowed) {
					jumpAllowed = false;
					anim.SetBool ("CubWalk", false);
					//	anim.SetBool ("Jumping", true);
					rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
				}
			} 
			//////////////////////////////////


			// Running ////////////////////
			if (Input.GetKey (KeyCode.LeftShift)) {
				cubRunnning = true;
				anim.SetBool ("CubWalk", false);
				//anim.SetBool ("CubRun", true);
				if (cubClimbing) {
					if (currentSpeed <= climbSpeed * 2) {
						currentSpeed = climbSpeed * 2;
					}
				} else {
					currentSpeed = walkSpeed * 2;
				}
			} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
				cubRunnning = false;
				if (cubClimbing) {
					currentSpeed = climbSpeed;
				} else {
					currentSpeed = walkSpeed;
				}
			}
			///////////////////////////////	


			// Walking ///////////////////
			if (!cubClimbing) {
				if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.A)) {
					anim.SetBool ("CubWalk", false);
				} else if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A)) {

					transform.Translate (new Vector3 (Time.deltaTime * currentSpeed * moveHorizontal, 0, 0));

					if (!cubRunnning && jumpAllowed) {
						currentSpeed = walkSpeed;
						anim.SetBool ("CubWalk", true);
					}

				}
			}
			///////////////////////////////	


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