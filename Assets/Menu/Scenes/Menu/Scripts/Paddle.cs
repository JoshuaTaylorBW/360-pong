using UnityEngine;
using System.Collections;
using System;

public class Paddle : MonoBehaviour {
	/*
	 *0 = blue
	 *1 = red
	 *2 = yellow
	 *3 = green
	 */

	public GameObject camera;
	public GameObject Win;

	public int startingScore;
	public int score;
	public int winScore;

	public int amountOfCircles;
	private bool alive = true;

	public bool player;
	public int whichPlayer;

	private int done = 0;
	public double speed = 0;
	public int color = 0;


	public int whatCircle = 0;
	public double currentAngle = 0;
	public GameObject Ball;
	public bool collided;
	private GameObject TheScore;


	// Use this for initialization
	void Start () {

		setAngle(currentAngle);
		transform.Rotate(new Vector3(0f, 0f, (float)currentAngle));	

		if(!Ball.GetComponent<Ball>().oneLoop()){
			Win = GameObject.Find("Scorebox");
			if(!Ball.GetComponent<Ball>().TwoPlayersOneColor){
				startingScore = Win.GetComponent<Variable>().winScore;
				score = startingScore;
			}else{
				winScore = Win.GetComponent<Variable>().winScore;
			}
		}

		score = startingScore;
		if(Ball == null){
			Debug.Log("unassigned");
			Ball = GameObject.Find("Ball");
		}
		TheScore = GameObject.Find("Score");
		amountOfCircles = Ball.GetComponent<Ball>().getAmountOfPlayers();
		if(speed == 0){
			speed = Ball.GetComponent<Ball>().startingSpeed * 40;
		}
	

	}

	void FixedUpdate(){
		if(alive){
			if(player){
				if(whichPlayer == 0){
					if(Input.GetAxis("Horizontal") < 0){
						currentAngle += speed;
						transform.Rotate(new Vector3(0f, 0f, (float)(speed)));	 
						
					}
					if(Input.GetAxis("Horizontal") > 0){
						currentAngle -= speed;
						transform.Rotate(new Vector3(0f, 0f, -(float)(speed)));	 
					}
					if(!Ball.GetComponent<Ball>().oneLoop()){
						if(Input.GetAxis("Vertical") > 0){
							if(done == 0){
								done++;
								if(transform.position.y > 0){
									moveUp();
								}else{
									moveDown();
								}
							}
						}
						if(Input.GetAxis("Vertical") < 0){
							if(done == 0){
								done++;
								if(transform.position.y > 0){
									moveDown();
								}else{
									moveUp();
								}
							}
						}
						if(Input.GetAxis("Vertical") == 0){
							done = 0;
						}
					}
				}else if(whichPlayer == 1){
					if(Input.GetAxis("Horizontal2") < 0){
						currentAngle += speed;
						transform.Rotate(new Vector3(0f, 0f, (float)(speed)));	 
						
					}
					if(Input.GetAxis("Horizontal2") > 0){
						currentAngle -= speed;
						transform.Rotate(new Vector3(0f, 0f, -(float)(speed)));	 
					}
					if(!Ball.GetComponent<Ball>().oneLoop()){
						if(Input.GetAxis("Vertical2") < 0){
							if(done == 0){
								done++;
							if(transform.position.y > 0){
								moveDown();
							}else{
								moveUp();
								}
							}
						}
							if(Input.GetAxis("Vertical2") > 0){
								if(done == 0){
									done++;
								if(transform.position.y > 0){
									moveUp();
								}else{
									moveDown();
								}
							}
						}
						if(Input.GetAxis("Vertical2") == 0){
							done = 0;
						}
					}
			}else if(whichPlayer == 2){
				if(Input.GetAxis("Horizontal3") < 0){
					currentAngle += speed;
					transform.Rotate(new Vector3(0f, 0f, (float)(speed)));	 
					
				}
				if(Input.GetAxis("Horizontal3") > 0){
					currentAngle -= speed;
					transform.Rotate(new Vector3(0f, 0f, -(float)(speed)));	 
				}
				if(!Ball.GetComponent<Ball>().oneLoop()){
					if(Input.GetAxis("Vertical3") < 0){
						if(done == 0){
							done++;
							if(transform.position.y > 0){
								moveDown();
							}else{
								moveUp();
							}
						} 
					}
					if(Input.GetAxis("Vertical3") > 0){
						if(done == 0){
							done++;
							if(transform.position.y > 0){
								moveUp();
							}else{
								moveDown();
							}
						}
					}
					if(Input.GetAxis("Vertical3") == 0){
						done = 0;
					}
			}
		}
	}
}
}

	void Update () {
		if(alive){
			if(Ball.GetComponent<Ball>().hasLost()){
				if(!Ball.GetComponent<Ball>().oneLoop()){
						if(!Ball.GetComponent<Ball>().TwoPlayersOneColor){
							if(Ball.GetComponent<Ball>().getColor() == color){
								if(score > 0){
									score--;
									Ball.GetComponent<Ball>().setLost(false);
								}else if(score < 1){
									transform.Rotate(new Vector3(0, 0, 90));
									renderer.material.color = Color.white;
									Ball.GetComponent<Ball>().amountOfPlayers--;
									TheScore.GetComponent<Score>().removeColors(color);
									Debug.Log("Dead");
									alive = false;
								}
							}
						}
					}
				}
			}
		transform.position = setAngle(currentAngle);

		if(Ball.GetComponent<Ball>().TwoPlayersOneColor){
			if(score >= winScore){
				Application.LoadLevel(0);
			}
		}
	}

	void moveUp(){
		if(amountOfCircles == 2){ 
			if(whatCircle < 1){
				whatCircle++;
				camera.GetComponent<ScreenShake>().ittyShake();
				System.Threading.Thread.Sleep(20);
			}
		}
		if(amountOfCircles == 3){
			if(whatCircle < 2){
				whatCircle++;
				camera.GetComponent<ScreenShake>().ittyShake();
				System.Threading.Thread.Sleep(20);
			}
		}
	}
	
	void moveDown(){
		if(whatCircle > 0){
			whatCircle--;
			camera.GetComponent<ScreenShake>().ittyShake();
			System.Threading.Thread.Sleep(20);
		}
	}

	void OnCollisionEnter(Collision col) {
		if(alive){
			topRightCollision(col);
			bottomRightCollision(col);
			bottomLeftCollision(col);
			topLeftCollision(col);
		}

		if(col.gameObject == Ball){
			Debug.Log("hit ball");
			if(Ball.GetComponent<Ball>().oneLoop() || Ball.GetComponent<Ball>().TwoPlayersOneColor){
			score++;
			}
		}

	}
	
	void topRightCollision(Collision col){
		if(col.gameObject.tag == "Paddle"){
			//top right
			//top
			if(transform.position.x >= 0 && transform.position.y >= 0){
				if(col.gameObject.transform.position.x < transform.position.x){
					if(col.gameObject.transform.position.y > transform.position.y){
						
						collided = true;
						Debug.Log("collided top right");
						currentAngle = currentAngle - 30;
						transform.Rotate(new Vector3(0f, 0f, (float)(-30)));	
						camera.GetComponent<ScreenShake>().smallShake();
						System.Threading.Thread.Sleep(20);
					}
			}
				//bottom
				if(col.gameObject.transform.position.x > transform.position.x){
					if(col.gameObject.transform.position.y < transform.position.y){
						collided = true;
						Debug.Log("collided top right");
						currentAngle = currentAngle + 30;
						transform.Rotate(new Vector3(0f, 0f, (float)(30)));	
						camera.GetComponent<ScreenShake>().smallShake();
						System.Threading.Thread.Sleep(20);
						
						
					}
				}
			}
		}
	}
	
	void bottomRightCollision(Collision col){
		if(col.gameObject.tag == "Paddle"){
			//top right
			//top
			if(transform.position.x > 0 && transform.position.y < 0){
				if(col.gameObject.transform.position.x > transform.position.x){
					if(col.gameObject.transform.position.y > transform.position.y){
						
						collided = true;
						Debug.Log("collided");
						currentAngle = currentAngle - 30;
						transform.Rotate(new Vector3(0f, 0f, (float)(-30)));	
						camera.GetComponent<ScreenShake>().smallShake();
						System.Threading.Thread.Sleep(20);
					}
				}
				//bottom
				if(col.gameObject.transform.position.x < transform.position.x){
					if(col.gameObject.transform.position.y < transform.position.y){
						collided = true;
						Debug.Log("collided");
						currentAngle = currentAngle + 30;
						transform.Rotate(new Vector3(0f, 0f, (float)(30)));	
						camera.GetComponent<ScreenShake>().smallShake();
						System.Threading.Thread.Sleep(20);
						
					}
				}
			}
		}
	}
	
	void bottomLeftCollision(Collision col){
		if(col.gameObject.tag == "Paddle"){
			//top right
			//top
			if(transform.position.x < 0 && transform.position.y < 0){
				if(col.gameObject.transform.position.x > transform.position.x){
					if(col.gameObject.transform.position.y < transform.position.y){

						
						collided = true;
						Debug.Log("collided");
						currentAngle = currentAngle - 30;
						transform.Rotate(new Vector3(0f, 0f, (float)(-30)));	
						camera.GetComponent<ScreenShake>().smallShake();
						System.Threading.Thread.Sleep(20);
					}
				}
				//bottom
				if(col.gameObject.transform.position.x < transform.position.x){
					if(col.gameObject.transform.position.y > transform.position.y){
						collided = true;
						Debug.Log("collided");
						currentAngle = currentAngle + 30;
						transform.Rotate(new Vector3(0f, 0f, (float)(30)));	
						camera.GetComponent<ScreenShake>().smallShake();
						System.Threading.Thread.Sleep(20);
						
						
					}
				}
			}
		}
	}
	
	void topLeftCollision(Collision col){
		if(col.gameObject.tag == "Paddle"){
			//top right
			//top
			if(transform.position.x < 0 && transform.position.y > 0){
				if(col.gameObject.transform.position.x < transform.position.x){
					if(col.gameObject.transform.position.y < transform.position.y){
						
						collided = true; 
						Debug.Log("collided");
						currentAngle = currentAngle - 30;
						transform.Rotate(new Vector3(0f, 0f, (float)(-30)));	
						camera.GetComponent<ScreenShake>().smallShake();
						System.Threading.Thread.Sleep(20);
					}
				}
				//bottom
				if(col.gameObject.transform.position.x > transform.position.x){
					if(col.gameObject.transform.position.y > transform.position.y){
						collided = true;
						Debug.Log("collided");
						currentAngle = currentAngle + 30;
						transform.Rotate(new Vector3(0f, 0f, (float)(30)));	
						camera.GetComponent<ScreenShake>().smallShake();
						System.Threading.Thread.Sleep(20);
						
						
					}
				}
			}
		}
	}
	
	void OnCollisionExit(Collision col){
		collided =false;
	}
	
	Vector3 setAngle(double angle){
		//circleX  and CircleY = center of circle / 100
		int circleX = 0;
		int circleY = 0;
		
		int radius = 3;
		
		if(whatCircle == 0){
			radius = 6;
		}else if(whatCircle == 1){
			radius = 5;
		}else if(whatCircle == 2){
			radius = 4;
		}

		double radian = angle * (Math.PI/180);
		
		return new Vector3(
			(float)(Math.Cos(radian) * radius + circleX),
			(float)(Math.Sin(radian) * radius + circleY),
			0f
			);
	}

	public int getColor(){
		return color;
	}

	public int getScore(){
		return score;
	}

}
