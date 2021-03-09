using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // scenemanagement required to load scenes

public class TriggerDead : MonoBehaviour {


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			Debug.Log ("Player Dead - reload scene");

			//find the audiomanager and play the sound
			if (AudioManager.instance != null) {
				AudioManager.instance.PlayThud();
			}

			//ReloadScene();
			StartCoroutine (ReloadScene ());
		}
	}

	private IEnumerator ReloadScene(){
		yield return new WaitForSeconds (0.1f);
		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);
	}

}
