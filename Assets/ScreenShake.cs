using UnityEngine;
using System.Collections;



public class ScreenShake : MonoBehaviour {

	public GameObject Camera;
	public Vector3 cameraInit;
	public double shake = 0;
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	private int stopper = 0; //0 = ready

	// Use this for initialization
	void Start () {
		cameraInit = Camera.transform.localPosition;
	}

	void startStopper(){
		if(stopper == 0){
			stopper++;
			///stopwatch.Start;
		}
	}

	float makeShakeAmount(){
		int choice = Random.Range(0, 2);
		Debug.Log("choice is " + choice);
		if(choice == 0){
			return -shakeAmount;
		}else{
			return shakeAmount;
		}
	}

	// Update is called once per frame
	void Update () {
		if(shake > 0){

			Camera.transform.localPosition = new Vector3(cameraInit.x + (Random.value * makeShakeAmount()), cameraInit.y + (Random.value * makeShakeAmount()), cameraInit.z);
			shake -= Time.deltaTime * decreaseFactor;
		}else{
			shake=0;
			Camera.transform.localPosition = cameraInit;
		}
	}


	public void ittyShake() {
		shake = .25;
	}
	public void tinyShake(){
		shake = .5;
	}

	public void smallShake () {
		shake = 1;
	}

	public void bigShake() {
		shake = 2;
	}

	public void sleep() {
		System.Threading.Thread.Sleep(1000);
	}

}
