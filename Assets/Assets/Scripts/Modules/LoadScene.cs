using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public void Load (int sceneIndex) {
		if (sceneIndex == 0)
			GameManager.simulate = true;
		
		SceneManager.LoadScene (sceneIndex);
	}

	public void Quit() {
		Application.Quit ();
	}
}
