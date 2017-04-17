using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager global;
	public static bool simulate = true;

	public GameObject obstacles;

	void Start () {
		global = this;
		Cursor.visible = false;
	}

	void Update () {
	}

	public void GameOver() {
		simulate = false;

		Animation anim = GetComponent<PlayerController> ().anim;
		if (anim.isPlaying)
			anim.Stop ();
		Cursor.visible = true;
		print ("GameOver");

		StartCoroutine ("ToMenu");
	}

	IEnumerator ToMenu() {
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene (0);
		simulate = true;
	}
}
