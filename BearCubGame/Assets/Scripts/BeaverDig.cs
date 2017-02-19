﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaverDig : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D col) {

		if (col.tag == "TreeABC") {


			GetComponentInParent<BeaverController> ().digAllowed = true;

			GetComponentInParent<BeaverController> ().SetTree (col.gameObject);

		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "TreeABC") {

			GetComponentInParent<BeaverController> ().digAllowed = false;

			GetComponentInParent<BeaverController> ().SetTree (null);
		}
	}
}
