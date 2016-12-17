using UnityEngine;
using System.Collections;

public class StoneController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D col) {

		if (col.tag == "BearCubPlayer") {
			col.GetComponent<BearCubController> ().pickupAvailable = true;
			col.GetComponent<BearCubController> ().pickUpObject = this.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "BearCubPlayer") {
			col.GetComponent<BearCubController> ().pickupAvailable = false;
			this.transform.GetComponent<PolygonCollider2D> ().enabled = true;
			this.transform.GetComponent<Rigidbody2D> ().gravityScale = 1;
			//	pickUpObject.transform.SetParent(this.transform);
		}
	}
}
