using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

	public GameObject Ball;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Ball.GetComponent<Ball>().gameStarted){
			renderer.enabled = false;
		}
	}
}
