using UnityEngine;
using System.Collections;

public class TreeSect : MonoBehaviour {

	public int uniqueNumTag;

	public int abcCount = 0;

	private TreeTrunk parentTrunkScript;

	// Use this for initialization
	void Start () {
	
		parentTrunkScript = this.gameObject.transform.parent.GetComponentInParent<TreeTrunk> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncreaseSectionCount() {

		abcCount++;

		// All sections gone //
		if (abcCount >= 3) {

			Destroy (this.transform.GetChild (0).gameObject);
		

			//	parentTrunkScript.CreateNewTrunk (uniqueNumTag, this.gameObject);
				parentTrunkScript.sectCollection (uniqueNumTag, this.gameObject);



			//Destroy (this.gameObject);

		}
	}

}
