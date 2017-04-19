using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReanudarButton : MonoBehaviour {

	public bool shouldCheck = false;

	void Update() {
		if (FeaturesEnabler.resume && shouldCheck) {
			check ();
			shouldCheck = false;
		}
	}

	void check() {
		if (GameManager.loser)
			gameObject.SetActive (false);
	}
}
