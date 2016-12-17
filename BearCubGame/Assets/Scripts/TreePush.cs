using UnityEngine;
using System.Collections;

public class TreePush : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (col.tag == "BisonCalfPlayer") {
			col.GetComponent<BisonCalfController> ().SetTree(this.gameObject);
			col.GetComponent<BisonCalfController> ().pushAllowed = true;
			Debug.Log ("BISON PUSH ALLOWED!!");
		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "BisonCalfPlayer") {
		//	col.GetComponent<BisonCalfController> ().SetTree(null);
			col.GetComponent<BisonCalfController> ().pushAllowed = false;
			Debug.Log ("BISON PUSH notALLOWED!!");
		}
	} 
}
