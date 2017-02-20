using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

	TreeABC treeABC;

	public float burnSpreadTimer = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {


		if (col.tag == "TreeABC") {
			treeABC = col.GetComponent<TreeABC> ();
			if (!treeABC.burning) {
				Debug.Log ("fireCatching");
				treeABC.SetTreeOnFire (burnSpreadTimer);
			}
		}


		if (col.tag == "BearCubPlayer") {
			Debug.Log ("bear in fire");
		}

	}
}
