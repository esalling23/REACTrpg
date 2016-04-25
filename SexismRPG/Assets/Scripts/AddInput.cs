using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AddInput : MonoBehaviour {

	private string playerInputString;
//	private int inputCount = 1;
	private GameObject inputManager;
	private GameObject button;
	// Use this for initialization
	void Awake () {
		inputManager = GameObject.Find ("InputManager");
//		inputCount = inputManager.GetComponent<InputEmailer> ().inputCount;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddResponse () {
		button = GameObject.Find ("RespondButton");
		playerInputString = this.GetComponent<InputField>().text;
		string newString = playerInputString;
//		inputManager.GetComponent<InputEmailer>().inputCount++;
		inputManager.GetComponent<InputEmailer>().inputList.Add (newString);
		Debug.Log ("added " + newString + " to storage");

		//reset
		this.GetComponent<InputField> ().text = "";

		button.SetActive (false);

	}
}
