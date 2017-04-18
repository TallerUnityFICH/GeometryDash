using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifebar : MonoBehaviour {

	public Slider barrita;
	// Use this for initialization
	void Start () {
		barrita = GetComponent<Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (GameManager.lifes)
		{
		case 1:
			barrita.value = 1;
			break;
		case 0:
			barrita.value = 0;
			break;
		case -1:
			barrita.value = -1;
			break;
		}
	}
}
