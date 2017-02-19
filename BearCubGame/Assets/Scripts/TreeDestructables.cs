using UnityEngine;
using System.Collections;

public class TreeDestructables : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (col.tag == "Stone") {
			//this.GetComponent<BoxCollider2D> ().enabled = false;
			Destroy(this.gameObject);
			//this.GetComponent<BoxCollider2D> ().enabled = false;

		}
	}

	void OnTriggerExit2D(Collider2D col) {

	} 
}
