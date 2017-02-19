using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DirtController : MonoBehaviour {

	public List<Transform> dirtTransList;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DeleteDirtList() {

		for (int i = 0; i < dirtTransList.Count; i++) {
			if (dirtTransList [i]) {
				dirtTransList [i].GetComponent<DirtDiggable> ().DestroyDirt ();
			}
		}

	}

	public void AddDirtToList(Transform dirtPiece) {

		dirtTransList.Add (dirtPiece);

	}

	public void RemoveDirtToList(Transform dirtPiece) {

		dirtTransList.Remove (dirtPiece);

	}
}
