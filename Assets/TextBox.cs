 using UnityEngine;
using System.Collections;

public class TextBox : MonoBehaviour {

	public string score = "7";
	private GUIStyle style;
	public float left, top;


	// Use this for initialization
	void Start () {
		style = new GUIStyle();
		style.fontSize = 35;
		style.normal.textColor = Color.white;
	}

	void OnGUI() {

		score = GUI.TextField(new Rect(left, top, 80, 80), score, 2, style);

	}
	// Update is called once per frame
	void Update () {
	
	}



}
