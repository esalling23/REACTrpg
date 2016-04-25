using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;  
using System;//Allows us to use Lists. 

public class GameManager : MonoBehaviour	{

	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
	public int level = 2;

	private GameObject inputControl;
	private String emailInput;
	private String nameInput;
	private GameObject player;
	public GameObject playerInfo;
	public GameObject inputEmail;
	public GameObject inputName;
	public bool inputReady = false;
	public Text buttonText;
	public GameObject levelImage;
	public GameObject exitInfo;

	private List<Person> people;
//	private bool peopleMoving;

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
//		DontDestroyOnLoad(gameObject);

		people = new List<Person>();

		//Get a component reference to the attached BoardManager script
		boardScript = GetComponent<BoardManager>();

		//Call the InitGame function to initialize the first level 
		InitGame();

		player = GameObject.Find ("Player");
		exitInfo.SetActive (false);

		inputControl = GameObject.Find ("InputManager");
	}

	void OnLevelWasLoaded(int index)
	{
		//Add one to our level number.
//		level = 8;
		//Call InitGame to initialize our level.
		StartMenu();
		Debug.Log ("start menu");
	}

	public void StartMenu() {
		player.GetComponent<PlayerControl> ().canMove = false;

	}

	public void StartGame () {
		Debug.Log ("Trying to start game");
		if (inputReady == true) {
			buttonText.GetComponent<Text>().text = "SENDING YOUR INFO...";
			Invoke ("HidePlayerInfo", 3f);
			Debug.Log ("init game");

		} else {
			buttonText.GetComponent<Text>().text = "PLEASE ENTER NAME AND EMAIL";
		}
	}

	//Initializes the game for each level.
	void InitGame() 
	{

		//Clear any Enemy objects in our List to prepare for next level.
		people.Clear ();

		//Call the SetupScene function of the BoardManager script, pass it current level number.
		boardScript.SetupScene (20);


	}

	public void PlayerInfo () {
		emailInput = inputEmail.GetComponent<InputField>().text;
		nameInput = inputName.GetComponent<InputField>().text;
		if (String.IsNullOrEmpty (emailInput) || String.IsNullOrEmpty (nameInput)) {
			
			inputReady = false;
		} else {
			inputControl.GetComponent<InputEmailer> ().playerEmail = emailInput;
			inputControl.GetComponent<InputEmailer> ().playerName = nameInput;
			inputReady = true;
			Debug.Log (emailInput + " ; " + nameInput);
		}
		Debug.Log ("Start button clicked");
		Debug.Log (inputReady);


	}

	void HidePlayerInfo() {
		playerInfo.SetActive(false);
		Invoke ("HideLevelImage", 8f);

		//Set levelImage to active blocking player's view of the game board during setup.

	}
	void HideLevelImage()
	{
		//Disable the levelImage gameObject.
		levelImage.SetActive(false);

		player.GetComponent<PlayerControl> ().CanMoveNow();
	}

	public void ExitMenu () {
		inputControl.GetComponent<InputEmailer> ().Mail ();
		player.GetComponent<PlayerControl> ().canMove = false;
		exitInfo.SetActive (true);
	}

	public void ExitGame() {
		Application.Quit();
	}

	//Update is called every frame.
	void Update()
	{
		
	}


}