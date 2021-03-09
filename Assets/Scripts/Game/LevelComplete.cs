using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour {

	public GameObject completeText;
	public GameObject timeText;

	void Awake () {
		
	}

	//we use the onenable & ondisable to subscribe to events
	//note the += & -= 
	void OnEnable(){
		TriggerWin.OnLevelComplete += TriggerWin_OnLevelComplete;
	}
	void OnDisable(){
		TriggerWin.OnLevelComplete -= TriggerWin_OnLevelComplete;
	}
	void TriggerWin_OnLevelComplete (){		

	}
}
