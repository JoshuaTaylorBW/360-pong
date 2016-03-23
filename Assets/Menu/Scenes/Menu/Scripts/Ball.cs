using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour {

	/*
	 *0 = blue
	 *1 = red
	 *2 = yellow
	 *3 = green
	 */
	public GameObject camera;

	//public bool mouseControlled;
	//public bool mouseFollower;
	public bool moving = false;
	public bool gameStarted = false;
	public bool lost = false;



	public bool randomStartSpeed;

	public int color;
	private GameObject Score;

	public int amountOfPlayers;
	private Vector3 startingDirection;

	private Vector3 movement;
	public float startingSpeed;
	public float currentSpeed;

	public int numberHits = 0;
	public string gameMode;
	public bool singleQuad;
	public bool singleDup;
	public bool TwoPlayersOneColor;

	void setGameMode () {
		if(gameMode == "singleQuad"){
			singleQuad = true;
		}
		if(gameMode == "Classic"){

		}
		if(gameMode == "SingleDup"){
			singleDup = true;
		}
		if(gameMode == "Two Players One Color"){
			TwoPlayersOneColor = true;
		}
	}
	public int getAmountOfPlayers(){
		return amountOfPlayers;
	}

	float makeStartingSpeed () {
		if(randomStartSpeed){
			float start;
			float tempStart = startingSpeed * 100;
			if(!oneLoop() && !TwoPlayersOneColor){
				start = Random.Range((int)0.01, (int)tempStart);
			}else{
				start = Random.Range(-(int)tempStart, (int)tempStart);
			}
			float reality = start / 100;
			float reality2 = reality / 1;
			Debug.Log("temp speed " + reality);
			return reality2 * 2;
		}else{
			float choiceX = Random.Range(0, 2);
			Debug.Log(choiceX);
			float answer = choiceX * startingSpeed;
			return answer;
		}
	}

	public Vector3 randomDirection(){
		int choiceX = Random.Range(0, 1);
		int choiceY = Random.Range(0, 1);
		Vector3 Answer = new Vector3(choiceX * startingSpeed, choiceY * startingSpeed, 0);
		Debug.Log(choiceX);
		return Answer;

	}

	public void startMoving(){
		float ySpeed;
		float xSpeed;
		lost = false;
		if(!oneLoop() && !TwoPlayersOneColor){
			newColor();
			if(color == 0)
			{
				xSpeed = makeStartingSpeed();
				ySpeed = 0;
			}
			else if(color == 1)
			{
				xSpeed = -makeStartingSpeed();
				ySpeed = 0;
			}
			else 
			{
				xSpeed = 0;
				ySpeed = -makeStartingSpeed();
			}
		}else{
			xSpeed = makeStartingSpeed();
		
			Debug.Log("XSpeed " + xSpeed);
			ySpeed = 0;

			if(xSpeed == 0){
				while(ySpeed == 0){
					ySpeed = makeStartingSpeed();
				}
			}else{
				ySpeed = makeStartingSpeed();
			}
		}
		Debug.Log("Yspeed " + ySpeed);
		currentSpeed = Mathf.Sqrt((xSpeed * xSpeed) + (ySpeed * ySpeed));
		Vector3 newDirection = new Vector3(xSpeed, ySpeed, 0);
			
			movement = newDirection;
			moving = true;
			
			}



	void Start () {
		Score = GameObject.Find("Score");
		color = Random.Range(0, amountOfPlayers);
		if(!TwoPlayersOneColor){
			if(color == 0){
				renderer.material.color = Color.blue;
			}else if(color == 1){
				renderer.material.color = Color.red;
			}else if(color == 2){
				renderer.material.color = Color.yellow;
			}else if(color == 3){
				renderer.material.color = Color.green;
			}else{
				renderer.material.color = Color.white;
			}
		}else{
			color = -1;
		}

		Debug.Log(oneLoop());

		randomDirection();
			
	}

	void OnCollisionEnter(Collision col) {
		if(!TwoPlayersOneColor){
			if(col.gameObject.GetComponent<Paddle>().getColor() ==  color){
				if(singleQuad || singleDup){
					checkHits();
					camera.GetComponent<ScreenShake>().tinyShake();
					System.Threading.Thread.Sleep(20);
				}else{
					camera.GetComponent<ScreenShake>().tinyShake();
					System.Threading.Thread.Sleep(20);
					movement = -movement;
					if(!singleDup){
					newColor();
					}
				}
			}
		}else{
			camera.GetComponent<ScreenShake>().tinyShake();
			movement = -movement;

		}
	}
	  
	void FixedUpdate(){
		if(moving){
			transform.Translate(movement);
		}
	}

	void Update () {
		if(!gameStarted && !moving){
			if(Input.GetButtonDown("Start")){
				startMoving();
				gameStarted = true;
			}else{
				transform.position = new Vector3(0, 0, 0);
			}
		}


		checkLose();

	}

	void checkLose(){
		if(transform.position.x > 11.85 || transform.position.x < -11.85 ||
		   transform.position.y > 7.2 || transform.position.y < -7.2){
			Debug.Log("Final speed " + currentSpeed);
			moving = false;
			gameStarted = false;
			camera.GetComponent<ScreenShake>().bigShake();
			System.Threading.Thread.Sleep(20);
			if(!oneLoop()){
				transform.position = new Vector3(0, 0, 0);
				rigidbody.freezeRotation = true;
				transform.rotation = new Quaternion(0, 0, 0, 0);
			}
			lost = true;



		}
	}

	void checkHits () {
		numberHits++;
		if(singleQuad){
			newColor();
		
				if(numberHits % 2 == 0){
					if(currentSpeed < .13 && currentSpeed > -.13){
						movement = -(movement * 1.2f);
					currentSpeed = Mathf.Sqrt((movement.x * movement.x) + (movement.y * movement.y));
					}else{

						movement = -movement;
					}
			
					numberHits = 0;
					
				}else{
				movement = -movement;
				
				}
			}else if(singleDup){
				movement = -movement;
			}
		}
	
	void newColor () {
		List<int> colors = Score.GetComponent<Score>().colorsLeft;
		int whichColor = 0;
		while(whichColor == color){
			whichColor = Random.Range(0, colors.Count);
		}
		color = colors[whichColor];
		if(color == 0){
			renderer.material.color = Color.blue;
		}else if(color == 1){
			renderer.material.color = Color.red;
		}else if(color == 2){
			renderer.material.color = Color.yellow;
		}else if(color == 3){
			renderer.material.color = Color.green;
		}
	}

	public bool singleQuadOn(){
		return singleQuad;
	}

	public bool singleDupOn(){
		return singleDup;
	}

	public int getNumberOfHits(){
		return numberHits;
	}

	public bool hasLost(){
		return lost;
	}
	public void setLost(bool l){
		lost = l;
	}
	public int getColor(){
		return color;
	}

	//returns whether or not we need vertical controls
	public bool oneLoop(){
		if(singleDup || singleQuad){
			return true;
		}
		return false;
	}



}
