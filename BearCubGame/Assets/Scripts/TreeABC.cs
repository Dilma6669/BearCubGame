using UnityEngine;
using System.Collections;

public class TreeABC : MonoBehaviour {

	public bool burning = false;
	private float burnSpreadTimer;
	private bool burnWait = false;


	public bool treeContact = false;
	public bool climbAllowed = true;

	public int treeHealth = 20;

	SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
	
		renderer = GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {


		if (treeHealth <= 0) {
			DestroyTree();
		}

		if (burning && (!burnWait)) {
			StartCoroutine (Wait(burnSpreadTimer));
		}


	}

	public void DestroyTree() {

		this.GetComponentInParent<TreeSect> ().IncreaseSectionCount ();

		 Destroy (this.gameObject);

	}
		

	public void SetTreeOnFire(float burnCool) {

		if (!burning) {

			burnSpreadTimer = burnCool;

			StartCoroutine (FirstWait (burnSpreadTimer));
		}
	}

	void OnTriggerStay2D(Collider2D col) {

		// enable bear to climb //
		if (col.tag == "BearCubPlayer" && climbAllowed) {
			col.GetComponent<BearCubController> ().climbAllowed = true;
			col.GetComponent<BearCubController> ().jumpAllowed = true;
		}

	}

	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "BearCubPlayer" & climbAllowed) {
			col.GetComponent<BearCubController> ().climbAllowed = false;
			col.GetComponent<BearCubController> ().cubClimbing = false;
		}
	}

	IEnumerator Wait(float secs) {
		burnWait = true;
		yield return new WaitForSeconds (secs);
		treeHealth--;
		burnWait = false;
	}

	IEnumerator FirstWait(float secs) {
		yield return new WaitForSeconds (secs);
		if (!burning) {
			burning = true;

			// have to load fire game object from resources folder //
			GameObject fire = (GameObject)Instantiate (Resources.Load ("FireTemp"));
			fire.transform.SetParent (this.transform, false);
			fire.transform.localScale = new Vector3 (1.5f, 1.5f, -3f);
		}
	}
}
