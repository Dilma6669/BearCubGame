using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeTrunk : MonoBehaviour {


	// 0 == Fully Standing
	// 1 == HAlf Broken
	// 2 == No Bottom, Fully Broken
	private int treeTrunkStatus = 0;
	private bool treeTrunkStatusSet = false;

	private int sectNumToCut = 0;
	private float createCoolDown = 0.2f;
	public bool createOK = true;

	private int countOffset = 0;
	private int sectCountTotal = 0;

	private List<int> sectList = new List<int>();
	private Component[] childComponents;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
	
		// Destroy the TreeTrunk if NotificationServices sections left //
		if (this.gameObject.transform.childCount == 0) {
			Destroy (this.gameObject);
		}

	}

	public void sectCollection(int uniqueNum, GameObject sectObject) {

		sectList.Add (uniqueNum);

		if (createOK) {
			StartCoroutine (Wait (createCoolDown));
		}

	}

	public void CreateNewTrunk() {

		/*
		countOffset++;

		Debug.Log ("recieved sect : " + uniqueNum);

		Debug.Log ("sectCountTotal " + (transform.childCount - countOffset));
		Debug.Log ("transform.childCount " + (transform.childCount));

		sectCountTotal = (transform.childCount - countOffset);

		if (uniqueNum == sectCountTotal) {
			// fully standing
			treeTrunkStatus = 0;
		} else if (uniqueNum == 0) {
			// half brokn
			treeTrunkStatus = 2;
		} else {
			// no bottom, full broken
			treeTrunkStatus = 1;
		}

		//	sectNumToCut = i;
/*
		if (treeTrunkStatus == 0) {

			Debug.Log ("sectList.size " + sectList.Count);
			foreach (GameObject element in sectList) {
				Destroy (element);
				countOffset--;
			}

			sectList.Clear();
		}
		*/

		foreach (int uniqueNum in sectList) {
			if (uniqueNum == transform.GetChild (transform.childCount - 1).GetComponent<TreeSect> ().uniqueNumTag) {
				// fully standing -Burn from top //
				treeTrunkStatus = 0;
				break;
			} else if (uniqueNum == transform.GetChild (0).GetComponent<TreeSect> ().uniqueNumTag) {
				// no bottom, full broken -Burn from bottom //
				treeTrunkStatus = 2;
				break;
			} else {
				// half brokn -Burn from middle //
				treeTrunkStatus = 1;
			}
		}
	
		switch (treeTrunkStatus) {
		case 0:
			Debug.Log ("FULLY STANDING");
			TreeFullyStanding ();
			break;
		case 1:
			Debug.Log ("HALF BROKEN");
			TreeHalfBroken ();
			break;
		case 2:
			Debug.Log ("NO BOTTOM");
			TreeNoBottom ();
			break;
		}



		// if fire at bottom or top of tree, no need to create new trunk //
	/*	if (sectNumToCut == transform.childCount-1) {

			Debug.Log ("safe tree");

			/*
				// setting broken trunk object to be walked on normally //
			childComponents = GetComponentsInChildren<BoxCollider2D> ();
			foreach (BoxCollider2D coll in childComponents) {
				coll.enabled = true;
			}
			// stopping bear from climbing once broken //
			childComponents = GetComponentsInChildren<TreeABC> ();
			foreach (TreeABC script in childComponents) {
				script.climbAllowed = false;
			}
			transform.GetComponent<Rigidbody2D> ().isKinematic = false;
			*/

		/*} else if (createOK) {


			createOK = false;
			
				Debug.Log ("CREATE NEW OBJECT");

			/*
				GameObject newTrunk = Instantiate (this.gameObject);

				Debug.Log ("sizeof trunk: " + transform.childCount);
				Debug.Log ("sizeof new trunk: " + newTrunk.transform.childCount);

				newTrunk.transform.SetParent (transform.parent.gameObject.transform, false);


				Debug.Log ("remove objects from original tree: ");
				//remove objects from original tree, all above cut //
				for (int i = sectNumToCut; i < transform.childCount; i++) {
					Destroy (transform.GetChild (i).gameObject);
					Debug.Log ("removing object : " + i);
				} 


				Debug.Log ("remove objects from new tree: ");
				//remove objects from new tree, all below cut //
			for (int i = sectNumToCut; i >= 0; i--) {
				Destroy (newTrunk.transform.GetChild (i).gameObject);
				Debug.Log ("removing object : " + i);
			}

				/*
				// setting broken trunk object to be walked on normally //
				childComponents = newTrunk.GetComponentsInChildren<BoxCollider2D> ();
				foreach (BoxCollider2D coll in childComponents) {
					coll.enabled = true;
				}
				// stopping bear from climbing once broken //
				childComponents = newTrunk.GetComponentsInChildren<TreeABC> ();
				foreach (TreeABC script in childComponents) {
					script.climbAllowed = false;
					//script.treeHealth = 2;
				}
				
				newTrunk.transform.GetComponent<Rigidbody2D> ().isKinematic = false;
				*/
		//} 

		//Debug.Log ("count RESET");
		//count = 0;

	}

	private void TreeFullyStanding(){

		int count = transform.childCount-1;
		foreach (int uniqueNum in sectList) {
			Destroy (transform.GetChild (count).gameObject);
			count--;
		}
		sectList.Clear ();

	}

	private void TreeHalfBroken(){

	}

	private void TreeNoBottom(){

		int count = 0;
		foreach (int uniqueNum in sectList) {
			Destroy (transform.GetChild (count).gameObject);
			count++;
		}
		sectList.Clear ();

		// setting broken trunk object to be walked on normally //
		childComponents = GetComponentsInChildren<BoxCollider2D> ();
		foreach (BoxCollider2D coll in childComponents) {
			coll.enabled = true;
		}
		// stopping bear from climbing once broken //
		childComponents = GetComponentsInChildren<TreeABC> ();
		foreach (TreeABC script in childComponents) {
			script.climbAllowed = false;
			//script.treeHealth = 2;
		}

		transform.GetComponent<Rigidbody2D> ().isKinematic = false;

	}


	IEnumerator Wait(float secs) {
		createOK = false;
		yield return new WaitForSeconds (secs);

		CreateNewTrunk ();
		createOK = true;
	} 
}
