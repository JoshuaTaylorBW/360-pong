using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Threading;

public class Menu : MonoBehaviour {
	public int secondsToFade;
	protected Animator animator;
	private int ready = 0;
	private Stopwatch stopwatch;
	public long elapsed;


	void Start()
	{
		stopwatch = new Stopwatch();
		stopwatch.Start();
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		animator.SetInteger("Ready", ready);
		if(stopwatch.ElapsedMilliseconds > (secondsToFade * 1000))
			{
			Application.LoadLevel(1);
			UnityEngine.Debug.Log("gone");
			}

		if(Input.GetButtonDown("Start"))
		  	{
			Application.LoadLevel(1);
			}

	}


}

