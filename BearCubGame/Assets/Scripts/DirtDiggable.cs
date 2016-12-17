using UnityEngine;
using System.Collections;

public class DirtDiggable : MonoBehaviour {


	public bool dirtContact = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DestroyDirt() {

		Destroy (this.gameObject);

	}

	void OnTriggerStay2D(Collider2D col) {

		if (col.tag == "RabbitBabyPlayer") {

			dirtContact = true;

			if (col.GetComponent<RabbitBabyController> ().digging == true) {

				DestroyDirt ();

			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {

		dirtContact = false;

	} 
}
