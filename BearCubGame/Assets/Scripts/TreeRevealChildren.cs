using UnityEngine;
using System.Collections;

public class TreeRevealChildren : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col) {

		if (col.tag == "Stone") {

			this.GetComponent<SpriteRenderer> ().enabled = false;
		

			for (int i = 0; i < this.transform.childCount; i++) {

				this.transform.GetChild (i).gameObject.SetActive (true);
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {

	
	} 
}
