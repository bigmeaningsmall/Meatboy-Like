using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour {

	private float elapsedTime;

	private bool timerOn = false;

	void Awake(){

	}
	void Start () {
		StartTimer ();
	}
	void OnEnable(){
		TriggerWin.OnLevelComplete += TriggerWin_OnLevelComplete;
	}
	void OnDisable(){
		TriggerWin.OnLevelComplete -= TriggerWin_OnLevelComplete;
	}
	void TriggerWin_OnLevelComplete (){		
		StopTimer ();
	}
		
	void Update () {
		if (timerOn) {
			Timer ();
		}
	}

	public void StartTimer(){
		timerOn = true;
	}
	public void StopTimer(){
		timerOn = false;
	}
	private void Timer(){
		elapsedTime += Time.deltaTime;
		ScoreKeeper.instance.SetLevelTime (elapsedTime);
	}
}
