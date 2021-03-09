using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour {

	[Range(0.5f, 4f)]
	public float speed = 2f;

	[Range(0.5f, 4f)]
	public int moveRange = 4;

	void Update () {
//		transform.position = new Vector3 (Mathf.PingPong (Time.time * speed, moveRange), transform.position.y, transform.position.z);
	}
}
