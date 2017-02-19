using UnityEngine;
using System.Collections;

public class TreeABC : MonoBehaviour {

	public bool treeContact = false;

	private bool entered = false;

	public bool climbAllowed = true;

	public bool onFire = false;

	private int treeHealth = 20;

	private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
	
		renderer = GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {


		if (treeHealth <= 0) {
			DestroyTree ();
		}

	}

	public void DestroyTree() {

		this.GetComponentInParent<TreeSect> ().IncreaseSectionCount ();

		 Destroy (this.gameObject);

	}

	public void TreeOnFire() {

		renderer.color = new Color (255, 0, 0, 1);

	}

	void OnTriggerStay2D(Collider2D col) {

		// enable bear to climb //
		if (col.tag == "BearCubPlayer" && climbAllowed) {
			col.GetComponent<BearCubController> ().climbAllowed = true;
			col.GetComponent<BearCubController> ().jumpAllowed = true;
		}
			
		if (col.tag == "TreeABC") {
			Debug.Log ("here");
			if (col.GetComponent<TreeABC> ().onFire) {
				TreeOnFire ();
				onFire = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "BearCubPlayer" & climbAllowed) {
			col.GetComponent<BearCubController> ().climbAllowed = false;
			col.GetComponent<BearCubController> ().cubClimbing = false;
		}
	} 
}
