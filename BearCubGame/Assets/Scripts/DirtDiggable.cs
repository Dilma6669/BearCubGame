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

	void OnTriggerEnter2D(Collider2D col) {

		if (col.tag == "RabbitBabyPlayer") {

			dirtContact = true;

			col.GetComponent<RabbitBabyController> ().AddDirtToList (this.transform);

		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "RabbitBabyPlayer") {

			dirtContact = false;

			col.GetComponent<RabbitBabyController> ().RemoveDirtToList (this.transform);

		}

	} 
}
