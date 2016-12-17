using UnityEngine;
using System.Collections;

public class TreeClimb : MonoBehaviour {

	GameObject player;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (col.tag == "BearCubPlayer") {
			//col.GetComponent<Player1Controller> ().SetAnimation(this.transform);
			col.GetComponent<BearCubController> ().climbAllowed = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "BearCubPlayer") {
			col.GetComponent<BearCubController> ().climbAllowed = false;
			col.GetComponent<BearCubController> ().cubClimbing = false;
		}
	} 
}
