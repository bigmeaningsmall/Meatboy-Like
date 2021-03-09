using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// This is an interface style class
/// it is used to update the user interface
/// </summary>

public class UI_Display : MonoBehaviour {

	[Header ("DISPLAY COMPONENTS")]
	public Text timerDisplay;
	public Text gemDisplay;

	public GameObject levelCompleteText;
	public GameObject levelCompleteTimeText;
	public GameObject restartLevelText;

	private Text completedTimeDisplay;

	private ScoreKeeper scoreKeeper;

	void Awake(){
		levelCompleteText.SetActive (false);
		levelCompleteTimeText.SetActive (false);
		restartLevelText.SetActive (false);
	}
	void Start () {
		//get the scorekeeper instance
		scoreKeeper = ScoreKeeper.instance;

		completedTimeDisplay = levelCompleteTimeText.GetComponent<Text> ();
	}

	void OnEnable(){
		TriggerWin.OnLevelComplete += TriggerWin_OnLevelComplete;
	}
	void OnDisable(){
		TriggerWin.OnLevelComplete -= TriggerWin_OnLevelComplete;
	}
	void TriggerWin_OnLevelComplete (){		
		StartCoroutine (DisplayFinalScore ());
	}

	public void Update(){
		DisplayTimer ();
		DisplayGems ();
	}
	public void DisplayTimer(){

		float t = scoreKeeper.GetLevelTime();

		timerDisplay.text = string.Format ("{0:#0}:{1:00}.{2:000}", 
			Mathf.Floor (t / 60),//minutes
			Mathf.Floor (t) % 60,//seconds
			Mathf.Floor ((t * 1000) % 1000));//miliseconds
	}


	public void DisplayGems(){
		
		int g = scoreKeeper.GetGemsCollected ();

		gemDisplay.text = ("GEMS:" + g.ToString ());
	}

	public IEnumerator DisplayFinalScore(){
		levelCompleteText.SetActive (true);

		yield return new WaitForSeconds (0.5f);

		float t = scoreKeeper.GetLevelTime();

		completedTimeDisplay.text = string.Format ("{0:#0}:{1:00}.{2:000}", 
			Mathf.Floor (t / 60),//minutes
			Mathf.Floor (t) % 60,//seconds
			Mathf.Floor ((t * 1000) % 1000));//miliseconds
		
		levelCompleteTimeText.SetActive (true);

		yield return new WaitForSeconds (0.5f);
		restartLevelText.SetActive (true);
	}
}
