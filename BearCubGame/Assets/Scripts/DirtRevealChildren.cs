using UnityEngine;
using System.Collections;

public class DirtRevealChildren : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (col.tag == "RabbitBabyPlayer") {

			col.GetComponent<RabbitBabyController> ().digAllowed = true;

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
