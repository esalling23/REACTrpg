using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Person : MonoBehaviour {

	private GameObject source;
	private Sprite thisImage;
	private GameObject thisEmotion;
	private GameObject thisDialogue;

	private GameObject control;
	private GameObject player;
	private GameObject canvas;

	private string thisComment;
	private int index = 0;
	public List<String> Comments = new List<String> ();
	public List<Sprite> ImagesList = new List<Sprite>();
	private string commentOne;
	private Image imageOne;
	private string commentTwo;
	private Image imageTwo;
	private string commentThree;
	private Image imageThree;
	private string commentFour;
	private Image imageFour;

	public GameObject dialogueBox;
	public GameObject emotionBox;

	private Transform target;

	void  Awake ()  
	{ 
		canvas = GameObject.Find ("UI");
//		camera = GameObject.Find ("MainCamera");
		control = GameObject.Find ("InputManager");

		player = GameObject.Find ("Player");
//		dialogueBox = GameObject.Find ("Dialoguing");
		emotionBox.SetActive (false);
		dialogueBox.SetActive (false);
//		AssignComments ();
//		AddCommentsList ();
	} 
	void Update ()  
	{  

	}  

	public void CheckMe () {

	}

	public void TalkToMe() {
//		zoomCamera = new Vector3 (player.transform.position
		dialogueBox.SetActive (true);
		ChooseRandomComment ();

	}



	public void Goodbye() {
		
		dialogueBox.SetActive (false);
		EmotionResponse ();
		Debug.Log ("goodbye");
//		Destroy (this.gameObject);
	}

	public void EmotionResponse () {
		emotionBox.SetActive (true);
		source = GameObject.Find ("SourceImage");
		thisImage = ImagesList [index];
		source.GetComponent<Image> ().sprite = thisImage;
		ImagesList.Remove (thisImage);
//		control.GetComponent<InputEmailer>().imagesList.Add (thisImage);
	}

	public void AddEmotion () {
		thisEmotion = GameObject.FindGameObjectWithTag ("Chosen");
		control.GetComponent<InputEmailer> ().emotionList.Add (thisEmotion.name);
		thisEmotion.tag = "Emotion";
		emotionBox.SetActive (false);
		player.GetComponent<PlayerControl> ().EndTalk();
	}


//	public void AddCommentsList() {
//		Comments.Add (commentOne);
////		Images.Add (imageOne);
//		Comments.Add (commentTwo);
////		Images.Add (imageTwo);
//		Comments.Add (commentThree);
////		Images.Add (imageThree);
//		Comments.Add (commentFour);
//		Debug.Log (Comments.Count);
////		Debug.Log (Images.Length);
//	}
//
//	public void AssignComments() {
//		commentOne = "OFFENSE COMMENT NUMBER ONE";
//		commentTwo = "OFFENSIVE COMMENT NUMBER TWO";
//		commentThree = "OFFENSIVE COMMENT NUMBER THREE";
//	}
//
	//	public void  

	public void ChooseRandomComment(){
		thisDialogue = GameObject.Find ("dialogueText");
		index = Random.Range (0, Comments.Count - 1);
		thisComment = Comments [index];
//		thisImage = Images [index];
		control.GetComponent<InputEmailer>().commentList.Add (thisComment);
		thisDialogue.GetComponent<Text> ().text = thisComment;
		Comments.Remove (thisComment);
	}
}
