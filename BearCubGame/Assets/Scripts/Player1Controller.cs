using UnityEngine;

public class Player1Controller : MonoBehaviour
{   
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


	private void FixedUpdate()
	{

		// Basic Movement Player //
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (climbAllowed) {
			
			if (Input.GetKey (KeyCode.W)) {

				cubClimbing = true;

				anim.SetBool ("CubClimb", true);

				this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
			}
		}

		if (!(climbAllowed)) {

			cubClimbing = false;

			anim.SetBool ("CubClimb", false);

			//ClearAnimation ();

			this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
		}
	


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

		//Sets Running animation
		if (Input.GetKey(KeyCode.LeftShift)) 
		{
			if (speed <= walkSpeed * 2) {
				speed = sprintSpeed;
			}
		} 
		else 
		{
			speed = walkSpeed;
		}


		if (climbAllowed) {

			transform.Translate (new Vector3 (0,  Time.deltaTime * (speed/2) * moveVertical, 0));

		}
			

		//Sets x and y basic movement
		transform.Translate (new Vector3 (Time.deltaTime * speed * moveHorizontal, 0, 0));


		//code for walking animation to flow fluently between x and z planes
		//anim.SetFloat ("WalkSpeedX", Mathf.Abs (moveHorizontal));
		//anim.SetFloat ("WalkSpeedZ", Mathf.Abs (moveVertical));

		//anim.SetBool ("Running", run);

		//if (!(cubClimbing)) {
		// Flip Player Over //
		turn (moveHorizontal);
		
		//}


	}

	//code 	for turning the player to either right or left.
	private void turn(float moveHorizontal)
	{
		if (moveHorizontal < 0 && !facingRight || moveHorizontal > 0 && facingRight) {

			rend.flipX = !rend.flipX;
			facingRight = !facingRight;
			//Vector3 playerScale = transform.localScale;
			//playerScale.x *= -1;
			//transform.localScale = playerScale;

		} 
	}
		

	/*
	public void SetAnimation(Transform trigger) {

		if (cubClimbing) {
		//anim.SetBool ("CubClimb", true);
			if (trigger.tag == "TreeClimableLeftMid") {
				facingRight = true;
				//anim.SetBool ("CubClimbLeftMid", true);
			}
			if (trigger.tag == "TreeClimableRightMid") {
				facingRight = false;
				//anim.SetBool ("CubClimbRightMid", true);
			}
		}
	}

	public void ClearAnimation() {
		
		anim.SetBool ("CubClimbLeftMid", false);
		anim.SetBool ("CubClimbRightMid", false);
	}
	*/

}