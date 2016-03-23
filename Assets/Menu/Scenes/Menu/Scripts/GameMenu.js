var isQuit=false;
var singleQuad=false;
var twoPlayers=false;
var threePlayers=false;
var singlePlayer=false;
var multiPlayer=false;
var TwoPOneC=false;
var ThreePOneC=false;
var MainMenu=false;
var classic=false;
var freeForAll=false;
var singleQuad_Pro=false;

function OnMouseEnter(){
selected = true;
renderer.material.color=Color.yellow;
}

function OnMouseExit(){
//change text color
renderer.material.color=Color.white;
}

function OnMouseUp(){
//is this quit
	if (isQuit==true) {
		Application.Quit();
	}else if(singleQuad){
		Application.LoadLevel(2);
	}else if(twoPlayers){
		Application.LoadLevel(5);
	}else if(threePlayers){
		Application.LoadLevel(3);
	}else if(singlePlayer){
		Application.LoadLevel(7);
	}else if(multiPlayer){
		Application.LoadLevel(8);
	}else if(TwoPOneC){
		Application.LoadLevel(6);
	}else if(ThreePOneC){
		Application.LoadLevel(4);
	}else if(MainMenu){
		Application.LoadLevel(1);
	}else if(freeForAll){
		Application.LoadLevel(9);
	}else if(classic){
		Application.LoadLevel(10);
	}else if(singleQuad_Pro){
		Application.LoadLevel(11);
	}
}

function Update(){
//quit game if escape key is pressed
if (Input.GetKey(KeyCode.Escape)) { Application.Quit();
}
}