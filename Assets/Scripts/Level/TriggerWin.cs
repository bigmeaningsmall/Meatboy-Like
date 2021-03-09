using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWin : MonoBehaviour {

	[Space(5f)]
	public GameObject collectParticles;

	[Range(0,10f)]
	public float particleOffset = 1.5f;

	private bool levelComplete;

	//this is an event delagate
	//it is broadcast globally
	//scripts/classes can subscribe using OnEnable / OnDisable to recieve the event
	public delegate void LevelComplete ();
	public static event LevelComplete OnLevelComplete;

	void Start () {

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			if (!levelComplete) {
				Debug.Log ("Level Complete");
				LevelCompleted ();
				CreateParticles ();

				//find the audiomanager and play the sound
				if (AudioManager.instance != null) {
					AudioManager.instance.PlayMusic();
				}
			}
		}
	}

	private void LevelCompleted(){
		//disable the collider to prevent multipe events
		gameObject.GetComponent<Collider2D> ().enabled = false;

		//braodcast the level complete event
		//check the event has a reciever or it can cause an error
		if (OnLevelComplete != null) {
			//broadcast the game over event
			OnLevelComplete ();
		}
	}
	private void CreateParticles(){
		//instantiate a particle system at the objects position & rotation
		Vector3 particlePosition = new Vector3 (transform.position.x, transform.position.y + particleOffset, transform.position.z);
		GameObject particlePrefab = Instantiate (collectParticles, particlePosition, Quaternion.identity);
	}
}