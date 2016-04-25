using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {
	
	public bool dialogue = false;
	public bool nextToPerson = false;

	private Camera theCamera;
	private GameObject control;
	public bool canMove = false;

	public GameObject person;
	private GameObject talker;
//	private string personSprite;
	public Sprite person1Off;
	public Sprite person2Off;
	public Sprite person3Off;
	public Sprite person4Off;

	public GameObject[] allPeople;
	public List<GameObject> People = new List<GameObject>();
	public Vector2 playerPosition = new Vector2();
	public Vector2 personPosition = new Vector2 ();

	private Vector2 personRight = new Vector2();
	private Vector2 personLeft = new Vector2();
	private Vector2 personUp = new Vector2();
	private Vector2 personDown = new Vector2();

//	private GameObject player;

	void Start() {
		control = GameObject.Find ("GameMaster");
		theCamera = GameObject.Find ("Main Camera").GetComponent <Camera> ();
	}

	// Update is called once per frame
	void Update () {
		if (dialogue == false && Input.GetButtonDown ("Jump")) {
			Talk ();
//			Debug.Log ("time to talk");
		}

//		if (Input.GetKeyDown (KeyCode.P)) {
//			People.Clear ();
//			exit = GameObject.Find ("ExitTile(Clone)");
//			exit.GetComponent<Exit> ().ExitLevel ();
//		}
	}

	public void CanMoveNow() {
		canMove = true;
	}
		
	public void Talk () {
		CheckForPerson ();
		if (nextToPerson == true) {
			dialogue = true;
			control.GetComponent<Person>().TalkToMe ();
			while (theCamera.orthographicSize > 3) {
				theCamera.orthographicSize --;

			}
			canMove = false;
			Debug.Log ("start a talkin'");

		} else {
			Debug.Log ("you are not next to anyone to talk to!");
		}


	}

	public void CheckForPerson () {
		playerPosition = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round (this.transform.position.y));

		personRight = playerPosition + new Vector2 (1, 0);
//		Debug.Log (personRight + " person, " + playerPosition + " player");
		personLeft = playerPosition + new Vector2 (-1, 0);
//		Debug.Log (personLeft + " person, " + playerPosition + " player");
		personUp = playerPosition + new Vector2 (0, 1);
//		Debug.Log (personUp + " person, " + playerPosition + " player");
		personDown = playerPosition + new Vector2 (0, -1);
//		Debug.Log (personDown + " person, " + playerPosition + " player");

		People.Clear ();
//		Debug.Log (People.Count + " is the count of People cleared");
		People.AddRange(GameObject.FindGameObjectsWithTag("Person"));
//		Debug.Log (People.Count + " is the count of People found");

		foreach (GameObject person in People) {
			personPosition = person.transform.position;
			Debug.Log (personPosition + " is the person");
//			Debug.Log (personRight + " is right of the player");
//			Debug.Log (personLeft + " is left of the player");
//			Debug.Log (personUp + " is above the player");
//			Debug.Log (personDown + " is below the player");


			if (personRight == personPosition) {
//				Debug.Log (personRight + " maybe, " + personPosition + " person");
				Debug.Log ("there is a person to your right!");	
				if (nextToPerson == false) {
					nextToPerson = true;
					if (person.name == "Person1(Clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person1Off;
					}
					if (person.name == "Person2(Clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person2Off;
					}
					if (person.name == "Person3(Clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person3Off;
					}
					if (person.name == "Person4(Clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person4Off;
					}
					person.tag = ("Spoken");
				}
			}
			if (personLeft == personPosition) {
//				Debug.Log (personLeft + " maybe, " + personPosition + " person");
				Debug.Log ("there is a person to your left!");	
				if (nextToPerson == false) {
					nextToPerson = true;
					if (person.name == "Person1(Clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person1Off;
					}
					if (person.name == "Person2(Clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person2Off;
					}
					if (person.name == "Person3(Clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person3Off;
					}
					if (person.name == "Person4(Clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person4Off;
					}
					person.tag = ("Spoken");
				}
			}
			if (personUp == personPosition) {
//				Debug.Log (personUp + " maybe, " + personPosition + " person");
				Debug.Log ("there is a person above you!");
				if (nextToPerson == false) {
					nextToPerson = true;
					if (person.name == "Person1(Clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person1Off;
					}
					if (person.name == "Person2(Clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person2Off;
					}
					if (person.name == "Person3(Clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person3Off;
					}
					if (person.name == "Person4(Clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person4Off;
					}
					person.tag = ("Spoken");
				}
			}
			if (personDown == personPosition) {
//				Debug.Log (personDown + " maybe, " + personPosition + " person");
				Debug.Log ("there is a person below you!");	
				if (nextToPerson == false) {
					nextToPerson = true;
					if (person.name == "Person1 (clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person1Off;
					}
					if (person.name == "Person2 (clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person2Off;
					}
					if (person.name == "Person3 (clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person3Off;
					}
					if (person.name == "Person4 (clone)") {
						person.GetComponent<SpriteRenderer> ().sprite = person4Off;
					}
					person.tag = ("Spoken");
				}
			}
		}
	}

	public void EndTalk() {
		
		nextToPerson = false;
		dialogue = false;
		while (theCamera.orthographicSize < 5) {
			theCamera.orthographicSize ++;
		}
		CanMoveNow ();
//		this.GetComponent<PlayerMovement> ().enabled = true;
//		control.GetComponent<GameManager> ().UItime = false;
//		Debug.Log (dialogue);

	}

	private void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Exit"){
			Debug.Log ("trying to exit..");
			People.Clear ();
//			Debug.Log (People.Count + " is the count of People cleared");
			People.AddRange(GameObject.FindGameObjectsWithTag("Person"));
			Debug.Log (People.Count + " is the count of People found");
			if (People.Count == 0) {
				Debug.Log ("level completed...");
				control.GetComponent<GameManager> ().ExitMenu ();
//				inputEmail.GetComponent<InputEmailer> ().Mail ();
			}
		}

	}
}
