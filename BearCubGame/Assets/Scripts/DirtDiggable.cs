using UnityEngine;
using System.Collections;

public class DirtDiggable : MonoBehaviour {

	DirtController dirtControllerScript;

	public bool dirtContact = false;

	// Use this for initialization
	void Start () {
	
		dirtControllerScript = GameObject.Find ("DirtController").gameObject.transform.GetComponent<DirtController> ();

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

			dirtControllerScript.AddDirtToList (this.transform);

		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if (col.tag == "RabbitBabyPlayer") {

			dirtContact = false;

			dirtControllerScript.RemoveDirtToList (this.transform);

		}

	} 
}
