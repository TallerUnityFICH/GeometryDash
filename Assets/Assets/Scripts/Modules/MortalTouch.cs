using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalTouch : MonoBehaviour {

	public bool fatal = false;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Equals ("Player"))
			GameManager.global.Damage (fatal);
	}
}
