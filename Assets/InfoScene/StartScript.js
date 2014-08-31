#pragma strict


var lives = 5;
var wait:int =500;
var count:int = 0;
function Start () {
	PlayerPrefs.SetInt("Lives", lives);
	PlayerPrefs.SetInt("Score", 0);
	PlayerPrefs.SetInt("Started",0);
	PlayerPrefs.SetInt("Level",1);
	guiText.text = "KinectWare";
	Screen.fullScreen = true;
}

function Update () {
	if(count > wait){
		Application.LoadLevel(1);
		
	}
	else{
		count +=1;
	}
}