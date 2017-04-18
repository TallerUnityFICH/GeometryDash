using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vidas : MonoBehaviour {

	public Slider barradelife;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.lifes == 2) {
			barradelife.value = GameManager.lifes;
		} else if (GameManager.lifes == 1) {
			barradelife.value = GameManager.lifes;
		} else {
			barradelife.value = GameManager.lifes;
		}
	}
}
