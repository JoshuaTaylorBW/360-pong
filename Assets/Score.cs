using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Score : MonoBehaviour {

	public GameObject Ball;
	public bool singlePlayer;
	private int score = 0;
	private int secondsLeft = 3;
	public GUIText[] texts = new GUIText[4];
	public GameObject[] Paddles = new GameObject[2];
	public List<int> colorsLeft = new List<int>();
	public Stopwatch stopper;
	/*
	 * blue
	 * red
	 * yellow
	 * green
	 * */

	void addColors(){
		colorsLeft.Add(0);
		colorsLeft.Add(1);
		if(Ball.GetComponent<Ball>().amountOfPlayers >= 3){
			colorsLeft.Add(2);
		}
		if(Ball.GetComponent<Ball>().amountOfPlayers >= 4){
			colorsLeft.Add(3);
		}
	}

	void Start () {
		stopper = new Stopwatch();
		if(Ball == null){
			Ball = GameObject.Find("Ball");
		}
		addColors();
		if(Ball.GetComponent<Ball>().oneLoop()){
			singlePlayer = true;
		}

	}

	void OnGui(){
		if(singlePlayer){
			GUI.contentColor = Color.white;

		}else{

		}
	}



	// Update is called once per frame
	void Update () {

		if(!singlePlayer){
			texts[0].text = Paddles[0].GetComponent<Paddle>().getScore().ToString();
			texts[1].text = Paddles[1].GetComponent<Paddle>().getScore().ToString();
				if(Ball.GetComponent<Ball>().amountOfPlayers >= 3){
					texts[2].text = Paddles[2].GetComponent<Paddle>().getScore().ToString();
				}
			}else{
				if(!Ball.GetComponent<Ball>().hasLost()){
					score = (Paddles[0].GetComponent<Paddle>().getScore() + Paddles[1].GetComponent<Paddle>().getScore() + Paddles[2].GetComponent<Paddle>().getScore() + Paddles[3].GetComponent<Paddle>().getScore());
					if(Ball.GetComponent<Ball>().gameStarted){
						texts[0].text = score.ToString();
					}else{
						texts[0].text = "\n \n Press Enter To Start";
					}
				}else{

					texts[0].text = "Good Job! \n You're score was " + score + "\n" + secondsLeft.ToString() + " seconds left until \n next round";
					if(stopper.ElapsedMilliseconds > 1000){
						secondsLeft--;
						stopper.Reset();
					}else{
						stopper.Start();
					}
					if(secondsLeft <= 0){
						Application.LoadLevel(1);
					}
				}
			}

		if(Input.GetButtonDown("Select")){
			Application.LoadLevel(1);
		}

	}	

	public void removeColors(int player){
		colorsLeft.Remove(player);
		checkLose();
	}

	List<int> RemainingColors(){
		return colorsLeft;
	}

	void checkLose(){
		if(colorsLeft.Count == 1){
			Application.LoadLevel(0);
		}
	}


}
