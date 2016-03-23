using UnityEngine;
using System.Collections;

using System;

public class Background : MonoBehaviour {

	private int NUMBER_OF_BACKGROUNDS = 3;
	private int whichPic = 0;
	protected Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		whichPic = UnityEngine.Random.Range(0, NUMBER_OF_BACKGROUNDS);
		Debug.Log(whichPic + "Backgorund");
		animator.SetInteger("Picture", whichPic);
		if(whichPic == 0){
			transform.position = new Vector3(-1.6f, 0f, 1f);
		}

	}
	
	// Update is called once per frame
	void Update () {
		//AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);


	}
}
