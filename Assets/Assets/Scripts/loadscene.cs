using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadscene : MonoBehaviour {

	public void LoadScene (int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}
	public void quit()
	{
		Application.Quit ();
	}
}
