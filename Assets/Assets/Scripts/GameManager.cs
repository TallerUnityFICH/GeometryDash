using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager global;
	public static bool simulate;
	public static bool loser;

	public GameObject obstacles;

	void Start () {
		global = this;
		Cursor.visible = false;
		simulate = true;
		loser = false;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			PlayPause ();
		}
	}

	public void GameOver() {
		simulate = false;
		GetComponent<PlayerController> ().anim.Stop ();
		loser = true;
		print ("GameOver");
	}

	public void PlayPause()
	{
		if (loser == false) 
		{
			if (simulate) 
			{
				simulate = false;
				Cursor.visible = true;
				GetComponent<PlayerController> ().anim.Stop ();
			} 
			else 
			{
				simulate = true;
				Cursor.visible = false;
				GetComponent<PlayerController> ().anim.Play ();
			}
		}
	}

	public void LoadScene (int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}
}
