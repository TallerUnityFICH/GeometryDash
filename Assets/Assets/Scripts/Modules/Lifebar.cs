using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour {

	RawImage image;

	void Start () {
		image = GetComponent<RawImage> ();
	}

	void Update () {
		Rect UV = image.uvRect;
		UV.x = 0.5f - 0.5f / (GameManager.maxLifes + 1) * (GameManager.lifes + 1);
		image.uvRect = UV;
	}
}
