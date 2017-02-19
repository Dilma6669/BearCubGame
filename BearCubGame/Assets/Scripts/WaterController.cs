using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {

	public bool treeInWater = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D col) {

		if (col.GetComponent<Rigidbody2D> ()) {
			Debug.Log ("Enter trigger");
			col.GetComponent<Rigidbody2D> ().gravityScale = -0.5f;
			col.GetComponent<Rigidbody2D> ().drag = 1f;
			col.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, col.GetComponent<Rigidbody2D> ().velocity.y);
		}
			
		if (col.tag == "TreeABC") {
			if (!treeInWater) {
				if (col.transform.parent.parent.GetComponentInChildren<Rigidbody2D> ()) {
					treeInWater = true;
					Debug.Log (col.gameObject);
					col.transform.parent.parent.GetComponentInChildren<Rigidbody2D> ().gravityScale = -0.5f;
					col.transform.parent.parent.GetComponentInChildren<Rigidbody2D> ().drag = 0.05f;
					col.transform.parent.parent.GetComponentInChildren<Rigidbody2D> ().velocity = new Vector2 (0.0f, col.transform.parent.parent.GetComponent<Rigidbody2D> ().velocity.y);
				//	col.transform.parent.parent.transform.Rotate(col.transform.parent.parent.transform.rotation.x, col.transform.parent.parent.transform.rotation.y, 90.0f);
				/*	if (col.transform.parent.parent.transform.rotation.z > -80.0f) {
						Debug.Log ("inside");
						col.transform.parent.parent.transform.Rotate(col.transform.parent.parent.transform.rotation.x, col.transform.parent.parent.transform.rotation.y, col.transform.parent.parent.transform.rotation.z+1);
					}

					if (col.transform.parent.parent.transform.rotation.z < -100.0f) {
						Debug.Log ("inside");
						col.transform.parent.parent.transform.Rotate(col.transform.parent.parent.transform.rotation.x, col.transform.parent.parent.transform.rotation.y, col.transform.parent.parent.transform.rotation.z-1);
					} */
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		Debug.Log ("leave trigger");
		if (col.GetComponent<Rigidbody2D> ()) {
			col.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
			col.GetComponent<Rigidbody2D> ().gravityScale = 1.5f;
		}

		if (col.tag == "TreeABC") {
			if (treeInWater) {
				if (col.transform.parent.parent.GetComponentInChildren<Rigidbody2D> ()) {
					treeInWater = false;
					Debug.Log (col.gameObject);
					col.transform.parent.parent.GetComponentInChildren<Rigidbody2D> ().gravityScale = 1.5f;
					col.transform.parent.parent.GetComponentInChildren<Rigidbody2D> ().drag = 0f;
					col.transform.parent.parent.GetComponentInChildren<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
				}
			} 
		}
	}
}
