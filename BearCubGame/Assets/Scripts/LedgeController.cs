using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D col) {

		if (col.tag == "BearCubPlayer") {
			col.GetComponent<BearCubController> ().jumpAllowed = true;
		} else 
		if (col.tag == "RabbitBabyPlayer") {
			col.GetComponent<RabbitBabyController> ().jumpAllowed = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "BearCubPlayer") {
			col.GetComponent<BearCubController> ().jumpAllowed = false;
		}
		if (col.tag == "RabbitBabyPlayer") {
			col.GetComponent<RabbitBabyController> ().jumpAllowed = false;
		}
	}
}
