using UnityEngine;
using System.Collections;

public class TreeTrunk : MonoBehaviour {

	private int sectNumToCut = 0;
	private float createCoolDown = 3.0f;
	private bool createOK = true;

	private Component[] colliders;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (this.gameObject.transform.childCount == 0) {
			Destroy (this.gameObject);
		}

	}

	public void CreateNewTrunk(GameObject sect) {

		if (createOK) {
			createOK = false;
			StartCoroutine (Wait (createCoolDown));


			for (int i = 0; i < transform.childCount; i++) {
				if (transform.GetChild (i).gameObject == sect) {
					sectNumToCut = i;
				}
			}

			Debug.Log ("create new object");

			GameObject newTrunk = Instantiate (this.gameObject);

			newTrunk.transform.SetParent (transform.parent.gameObject.transform, false);

			//remove objects from original tree, all above cut //
			for (int i = sectNumToCut; i < transform.childCount; i++) {
				Destroy (this.gameObject.transform.GetChild (i).gameObject);
			} 

			//remove objects from new tree, all below cut //
			for (int i = sectNumToCut; i >= 0; i--) {
				Destroy (newTrunk.transform.GetChild (i).gameObject);
			} 
			
			// setting broken trunk object to be walked on normally //
			colliders = newTrunk.GetComponentsInChildren<BoxCollider2D> ();
			foreach (BoxCollider2D coll in colliders) {
				coll.enabled = true;
			}
			// stopping bear from climbing once broken //
			colliders = newTrunk.GetComponentsInChildren<TreeABC> ();
			foreach (TreeABC script in colliders) {
				script.climbAllowed = false;
			}


			newTrunk.transform.GetComponent<Rigidbody2D> ().isKinematic = false;
		}

	}

	IEnumerator Wait(float secs) {
		createOK = false;
		yield return new WaitForSeconds (secs);
		createOK = true;
	}
}
