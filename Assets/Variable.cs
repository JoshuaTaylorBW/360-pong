using UnityEngine;
using System.Collections;

public class Variable : MonoBehaviour {

	public GameObject guiTextBox;
	public int winScore;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);


	}
	
	// Update is called once per frame
	void Update () {
		if(guiTextBox != null){
			winScore = int.Parse(guiTextBox.GetComponent<TextBox>().score);
		}
	}
}
