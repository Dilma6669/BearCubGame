using UnityEngine;
using System.Collections;

public class HoleEnter : MonoBehaviour {

	RabbitHoles rabbitHoles;

	// Use this for initialization
	void Start () {
	
		rabbitHoles = GameObject.Find ("RabbitHoles").gameObject.transform.GetComponent<RabbitHoles> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col) {

		if (col.tag == "RabbitBabyPlayer") {
			rabbitHoles.RabbitHoleBeingUsed (this.transform);
			col.GetComponent<RabbitBabyController> ().holeEnterAllowed = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "RabbitBabyPlayer") {
			rabbitHoles.RabbitHoleBeingUsed (null);
			col.GetComponent<RabbitBabyController> ().holeEnterAllowed = false;
		}
	} 
}
