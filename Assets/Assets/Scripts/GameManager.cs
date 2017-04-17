using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		GetComponent<PlayerController> ().anim.Stop ();
		Cursor.visible = true;
		print ("GameOver");
	}
}
