using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	private float t;

	private bool init = true;

	void Start () {
		
	}
		
	public float CountUpTimer () {
		t += Time.deltaTime;
		return t;
	}

	public float CountDownTimer (float startTime) {
		if (init) {
			init = false;
		}
		t -= Time.deltaTime;
		return t;
	}

	public void StopTimers(){
		init = true;

	}
}
