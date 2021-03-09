using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;
	
	public AudioSource music;
	public AudioSource jump;
	public AudioSource collect;
	public AudioSource thud;

	private void Awake() {
		instance = this;
	}

	public void PlayMusic () {
		music.Play ();
	}
	public void PlayJump() {
		jump.Play ();
	}
	public void PlayCollect () {
		collect.Play ();
	}
	public void PlayThud () {
		thud.Play ();
	}
}
