using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

	//Make the class a static we can access by instancing in any other class
	//this is the simpliest version of a 'singleton' design pattern
	//there can be only 1 singleton class instance - duplication will cause errors
	//an advanced singleton implementation would involve error checking for code safety
	public static ScoreKeeper instance; 

	private float elapsedTime;
	private int gems;

	void Awake () {
		//make this class an instance on Awake for singleton
		// to call the class : "ScoreKeeper.instance. " ....access variables or public functions
		instance = this;
	}

	public void SetLevelTime (float t) {
		elapsedTime = t;
	}
	public float GetLevelTime () {
		return elapsedTime;
	}
	public void SetGemsCollected (int g) {
		gems = gems + g;
	}
	public int GetGemsCollected () {
		return gems;
	}
}
