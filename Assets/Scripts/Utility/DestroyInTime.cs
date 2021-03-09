using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInTime : MonoBehaviour {

	public int timeToDestroy = 2;

	void Start () {
		if (timeToDestroy > 0) {
			Invoke ("DestroyGameObject", timeToDestroy);
		}
	}

	void DestroyGameObject () {
		Destroy (gameObject);
	}
}
