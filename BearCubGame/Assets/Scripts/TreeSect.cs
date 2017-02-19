using UnityEngine;
using System.Collections;

public class TreeSect : MonoBehaviour {

	public int sectionCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncreaseSectionCount() {

		sectionCount++;

		// All sections gone //
		if (sectionCount >= 3) {

			Destroy (this.transform.GetChild (0).gameObject);

			Destroy (this.gameObject);

			this.gameObject.transform.parent.GetComponentInParent<TreeTrunk> ().CreateNewTrunk (this.gameObject);



			//GetComponentInParent<TreeSect> ().IncreaseSectionCount ();

		}
	}

}
