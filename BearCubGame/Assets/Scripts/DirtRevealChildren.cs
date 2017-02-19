using UnityEngine;
using System.Collections;

public class DirtRevealChildren : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col) {

		if (col.tag == "RabbitBabyPlayer") {

			col.GetComponent<RabbitBabyController> ().digAllowed = true;
			col.GetComponent<RabbitBabyController> ().jumpAllowed = true;

			if (col.GetComponent<RabbitBabyController> ().digging) {

				this.GetComponent<BoxCollider2D> ().enabled = false;
				this.GetComponent<SpriteRenderer> ().enabled = false;

			}

			for (int i = 0; i < this.transform.childCount; i++) {
				
				this.transform.GetChild (i).gameObject.SetActive(true);

			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "RabbitBabyPlayer") {
			col.GetComponent<RabbitBabyController> ().digAllowed = false;
		}
	} 
}
