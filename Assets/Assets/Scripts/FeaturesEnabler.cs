using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeaturesEnabler : MonoBehaviour {

	public static bool color = false;
	public static bool points = false;
	public static bool death = false;
	public static bool menu = false;
	public static bool health = false;
	public static bool resume = false;

	static bool instructionsShown = false;

	bool ready = false;

	GameObject options;
	GameObject instructions;

	void Start () {
		options = GameObject.Find ("Options");

		instructions = GameObject.Find ("ButtonHelp");

		GameObject.Find ("Instructions").GetComponent<Toggle> ().isOn = instructionsShown;
		GameObject.Find ("Color").GetComponent<Toggle> ().isOn = color;
		GameObject.Find ("Contador").GetComponent<Toggle> ().isOn = points;
		GameObject.Find ("DeathAnim").GetComponent<Toggle> ().isOn = death;
		GameObject.Find ("MenuAnim").GetComponent<Toggle> ().isOn = menu;
		GameObject.Find ("Health").GetComponent<Toggle> ().isOn = health;
		GameObject.Find ("Resume").GetComponent<Toggle> ().isOn = resume;

		options.SetActive (false);
		instructions.SetActive (instructionsShown);

		ready = true;
	}

	void Update () {
		
	}

	public void Instructions() {
		if (ready)
			instructions.SetActive (instructionsShown = !instructionsShown);
	}

	public void switchOptions() {
		if (ready)
			options.SetActive (!options.activeSelf);
	}

	public void switchColor() {
		if (ready)
			color = !color;
	}

	public void switchPoints() {
		if (ready)
			points = !points;
	}

	public void switchDeath() {
		if (ready)
			death = !death;
	}

	public void switchMenu() {
		if (ready)
			menu = !menu;
	}

	public void switchHealth() {
		if (ready)
			health = !health;
	}

	public void switchResume() {
		if (ready)
			resume = !resume;
	}
}
