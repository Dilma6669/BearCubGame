using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	GameObject bearCubPlayer;
	GameObject rabbitBabyPlayer;
	GameObject bisonCalfPlayer;

	BearCubController bearCubController;
	RabbitBabyController rabbitBabyController;
	BisonCalfController bisonCalfController;

	GameObject MoveableTrees;

	public bool bearCubSelected = true;
	public bool rabbitBabySelected = false;
	public bool bisonCalfSelected = false;

	// Use this for initialization
	void Start () {

		MoveableTrees = GameObject.Find ("MoveableTrees").gameObject;
	
		bearCubPlayer = this.gameObject.transform.GetChild (0).gameObject;
		rabbitBabyPlayer = this.gameObject.transform.GetChild (1).gameObject;
		bisonCalfPlayer = this.gameObject.transform.GetChild (2).gameObject;

		bearCubController = bearCubPlayer.GetComponent<BearCubController> ();
		rabbitBabyController = rabbitBabyPlayer.GetComponent<RabbitBabyController>();
		bisonCalfController = bisonCalfPlayer.GetComponent<BisonCalfController>();

		SetColliders ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("1")) {

			bearCubController.CharacterActive = true;
			rabbitBabyController.CharacterActive = false;
			bisonCalfController.CharacterActive = false;

			bearCubSelected = true;
			rabbitBabySelected = false;
			bisonCalfSelected = false;
		}

		if (Input.GetKeyDown ("2")) {
			
			bearCubController.CharacterActive = false;
			rabbitBabyController.CharacterActive = true;
			bisonCalfController.CharacterActive = false;

			bearCubSelected = false;
			rabbitBabySelected = true;
			bisonCalfSelected = false;
		}

		if (Input.GetKeyDown ("3")) {

			bearCubController.CharacterActive = false;
			rabbitBabyController.CharacterActive = false;
			bisonCalfController.CharacterActive = true;

			bearCubSelected = false;
			rabbitBabySelected = false;
			bisonCalfSelected = true;
		}
	}

	private void SetColliders() {

		for (int i = 0; i < MoveableTrees.transform.childCount; i++) {

			Physics2D.IgnoreCollision (bearCubPlayer.GetComponent<BoxCollider2D> (), MoveableTrees.transform.GetChild (i).GetComponent<BoxCollider2D> ());
		}

		for (int i = 0; i < MoveableTrees.transform.childCount; i++) {

			Physics2D.IgnoreCollision (rabbitBabyPlayer.GetComponent<BoxCollider2D> (), MoveableTrees.transform.GetChild (i).GetComponent<BoxCollider2D> ());
		}

		for (int i = 0; i < MoveableTrees.transform.childCount; i++) {

			Physics2D.IgnoreCollision (bisonCalfPlayer.GetComponent<BoxCollider2D> (), MoveableTrees.transform.GetChild (i).GetComponent<BoxCollider2D> ());
		}

	}
}
