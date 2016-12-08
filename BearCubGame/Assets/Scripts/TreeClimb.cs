using UnityEngine;
using System.Collections;

public class TreeClimb : MonoBehaviour {

	GameObject player;
	// Use this for initialization
	void Start () {
	
		player = GameObject.Find ("Player").gameObject;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (col.tag == "Player") {
			//col.GetComponent<Player1Controller> ().SetAnimation(this.transform);
			col.GetComponent<Player1Controller> ().climbAllowed = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "Player") {
			col.GetComponent<Player1Controller> ().climbAllowed = false;
		}
	} 
}
