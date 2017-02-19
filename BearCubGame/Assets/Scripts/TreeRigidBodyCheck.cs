using UnityEngine;
using System.Collections;

public class TreeRigidBodyCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col) {

		if (col.tag == "Stone") {
			
		//	this.GetComponent<BoxCollider2D> ().enabled = false;
			this.GetComponent<Rigidbody2D> ().isKinematic = false;
	

		}


	}

	void OnTriggerExit2D(Collider2D col) {


	} 
}
