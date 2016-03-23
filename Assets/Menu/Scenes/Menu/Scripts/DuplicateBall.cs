using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DuplicateBall : MonoBehaviour {

	public Transform prefab;
	private Queue<Transform> objectQueue;
	public int numberOfObjects;
	public int done = 0;
	public int spawns = 0;

	// Use this for initialization
	void Start () {
		objectQueue = new Queue<Transform>(numberOfObjects);

	}

	void Update () {
		if(GameObject.Find("Ball").GetComponent<Ball>().getNumberOfHits() % 3 == 0){
			if(GameObject.Find("Ball").GetComponent<Ball>().getNumberOfHits() > 0){
			makeNewBall();
			}
		}else{
			done = 0;
		}
	}

	void makeNewBall(){
	if(done == 0){
		
		done++;
		if(spawns > 0){
				objectQueue.Enqueue((Transform)Instantiate(
					prefab, new Vector3(0f, (float)(done * 5), -0f), Quaternion.identity));
					prefab.GetComponent<Ball>().startMoving();
			}else{
				objectQueue.Enqueue((Transform)Instantiate(
					prefab, new Vector3(0f, (float)(done * 5), -0f), Quaternion.identity));
				prefab.GetComponent<Ball>().startMoving();
			}
		spawns++;
			}
		}
}
