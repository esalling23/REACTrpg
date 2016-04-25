using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement: MonoBehaviour {

	public const float stepDuration = 0.5f;
	public Coroutine playerMovement;
	public Sprite playerRight;
	public Sprite playerLeft;
	public Sprite playerUp;
	public Sprite playerDown;
	private Vector2 startPosition = new Vector2();
	public Vector2 destinationPosition = new Vector2();
	public Vector2 colliderPosition = new Vector2 ();
	public bool collided = false;
//	public bool nextToPerson = false;

	public GameObject[] houseTiles;
	public GameObject[] outerWallTiles;
	public GameObject[] personTiles;

	private GameObject control;

	void Start() {
		houseTiles = GameObject.FindGameObjectsWithTag ("House");
		outerWallTiles = GameObject.FindGameObjectsWithTag ("OuterWall");
		personTiles = GameObject.FindGameObjectsWithTag ("Person");

		this.GetComponent<SpriteRenderer> ().sprite = playerRight;
		control = GameObject.Find ("GameMaster");
	}

	void Update()
	{
//		this.transform.position = new Vector2 (Mathf.Round (this.transform.position.x), Mathf.Round (this.transform.position.y));

		if (this.GetComponent<PlayerControl> ().canMove == true) {
			if (playerMovement == null) {
				if (Input.GetButtonDown ("Up")) {       //In general not a good idea to use Input.GetKey; use Input.GetButton instead
					playerMovement = StartCoroutine (Move (Vector2.up));
					this.GetComponent<SpriteRenderer> ().sprite = playerUp;
				} else if (Input.GetButtonDown ("Down")) {
					playerMovement = StartCoroutine (Move (Vector2.down));
					this.GetComponent<SpriteRenderer> ().sprite = playerDown;
				} else if (Input.GetButtonDown ("Right")) {
					playerMovement = StartCoroutine (Move (Vector2.right));
					this.GetComponent<SpriteRenderer> ().sprite = playerRight;
				} else if (Input.GetButtonDown ("Left")) {
					playerMovement = StartCoroutine (Move (Vector2.left));
					this.GetComponent<SpriteRenderer> ().sprite = playerLeft;
				}
			}
		}
	}

	public void CheckDestinationPosition () {
//		nextToPerson = false;
		collided = false;
//		control.GetComponent<BoardManager> ().wallTiles;
		foreach (GameObject house in houseTiles) {
			colliderPosition = house.transform.position;
//			Debug.Log (colliderPosition + " and " + destinationPosition);
			if (Mathf.Round(destinationPosition.x) == colliderPosition.x && Mathf.Round(destinationPosition.y) == colliderPosition.y) {
				collided = true;
			}
		}
		foreach (GameObject outerWall in outerWallTiles) {
			colliderPosition = outerWall.transform.position;
//			Debug.Log (colliderPosition + " and " + destinationPosition);
			if (Mathf.Round(destinationPosition.x) == colliderPosition.x && Mathf.Round(destinationPosition.y) == colliderPosition.y) {
				collided = true;
			}
		}
		foreach (GameObject person in personTiles) {
			colliderPosition = person.transform.position;
//			Debug.Log (colliderPosition + " and " + destinationPosition);
			if (Mathf.Round(destinationPosition.x) == colliderPosition.x && Mathf.Round(destinationPosition.y) == colliderPosition.y) {
				collided = true;
			}
		}
	}

	public IEnumerator Move(Vector2 direction)
	{
		
		startPosition = this.transform.position;
		destinationPosition = startPosition + direction;

		Debug.Log (startPosition + " starting, " + destinationPosition + " ending");

		CheckDestinationPosition ();
		Debug.Log(collided);
		float t = 0.0f;

		if (collided == false) {
			while (t < 1.0f) {
				transform.position = Vector2.Lerp (startPosition, destinationPosition, t);
				t += Time.deltaTime / stepDuration;
				yield return new WaitForEndOfFrame ();
			}
			this.transform.position = new Vector2 (Mathf.Round (this.transform.position.x), Mathf.Round (this.transform.position.y));


			//transform.position = destinationPosition;
		} else {
			Debug.Log ("cannot move");

		}
//		collided = false;
		playerMovement = null;
	}


}
