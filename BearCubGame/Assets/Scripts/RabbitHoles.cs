using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RabbitHoles : MonoBehaviour {

	public List<Transform> rabbitHoleList;

	private Transform holeBeingUsedTrans = null;

	private bool canEnter = true;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < this.transform.childCount; i++) {

			rabbitHoleList.Add(this.transform.GetChild (i).gameObject.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public Vector3 EnterRabbitHole(Transform pTrans) {

		if (canEnter) {
			canEnter = false;
			StartCoroutine (Wait (1.0f));

			Vector3 playerTrans = pTrans.position;
			Vector3 newTrans = new Vector3 ();
			Transform temp = null;

			int rand = Random.Range (0, rabbitHoleList.Count);

			// if randomly selects same hole entering to exit //
			if(holeBeingUsedTrans == rabbitHoleList [rand]) {
				rand++;
				if (rand >= rabbitHoleList.Count) {
					rand = 0;
				}
			}

			temp = rabbitHoleList [rand];

			newTrans.x = temp.position.x;
			newTrans.y = temp.position.y;
			newTrans.z = temp.position.z * 0;

			return newTrans;

		} else {
			return pTrans.position;
		}
	}

	public void RabbitHoleBeingUsed(Transform holeTrans) {
		
		holeBeingUsedTrans = holeTrans;
	}

	IEnumerator Wait(float secs) {
		yield return new WaitForSeconds (secs);
		canEnter = true;
	}

}
