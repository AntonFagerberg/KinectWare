#pragma strict

var games = [2, 3, 4];
var wait:int = 160;
var count:int = 0;
var win:AudioSource;
var lose:AudioSource;

function Start () {	
	count = 0;
    var audio = GetComponents(AudioSource);
    win = audio[0];
    lose = audio[1];
    
    if(PlayerPrefs.GetInt("Started").Equals(0)){
	guiText.text = "Starting game" + "\nScore: " + PlayerPrefs.GetInt("Score") + "\nLives: " + PlayerPrefs.GetInt("Lives");
	}
	else{
	if(PlayerPrefs.GetInt("Won").Equals(0)){
	guiText.text = "Game Lost" + "\nScore: " + PlayerPrefs.GetInt("Score") + "\nLives: " + PlayerPrefs.GetInt("Lives");
	lose.Play();
	}
	
	else if(PlayerPrefs.GetInt("Won").Equals(1)){
	guiText.text = "Game Won" + "\nScore: " + PlayerPrefs.GetInt("Score") + "\nLives: " + PlayerPrefs.GetInt("Lives");
	win.Play();}
	}
	
}


//function Awake () {
//		DontDestroyOnLoad (transform.gameObject);
//	}

function Update () {
	if(PlayerPrefs.GetInt("Lives") < 0){
	guiText.text = "Game Over";
	lose.Play();
	wait=300;
	if( count == wait -1){
		Application.LoadLevel(0);
	}
	}
//	else if(PlayerPrefs.GetInt("Started").Equals(0)){
//	guiText.text = "Starting game" + "\nScore: " + PlayerPrefs.GetInt("Score") + "\n Lives left: " + PlayerPrefs.GetInt("Lives");
//	}
//	else{
//	if(PlayerPrefs.GetInt("Won").Equals(0)){
//	guiText.text = "Game Lost" + "\nScore: " + PlayerPrefs.GetInt("Score") + "\nLives left: " + PlayerPrefs.GetInt("Lives");
//	lose.Play();
//	}
//	
//	else if(PlayerPrefs.GetInt("Won").Equals(1)){
//	guiText.text = "Game Won" + "\nScore: " + PlayerPrefs.GetInt("Score") + "\nLives left: " + PlayerPrefs.GetInt("Lives");
//	win.Play();}
//	}
	
	if( count < wait){
		count += 1;}
	else{
	//Debug.Log("Random: " + Random.Range(2, Application.levelCount) + "Levelcount: " + Application.levelCount);
	var nextLevel = (5 - PlayerPrefs.GetInt("Lives") + PlayerPrefs.GetInt("Score")) % (Application.levelCount - 2);
	nextLevel += 2;
	Application.LoadLevel(nextLevel);
	PlayerPrefs.SetInt("Started",1);
	
	}
	
	
	
	}


	
	

