using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSlider : MonoBehaviour {

	public float scrollSpeed = 1;
	private Rect savedOffset;

	void Start () {
		savedOffset = GetComponent<RawImage>().uvRect;
	}

	void Update () {
		if (GameManager.simulate) {
			float x = Mathf.Repeat (Time.time * 0.1f * scrollSpeed, 1);
			Rect offset = savedOffset;
			offset.x = x;
			GetComponent<RawImage> ().uvRect = offset;
		}
	}

	void OnDisable () {
		GetComponent<RawImage>().uvRect = savedOffset;
	}
}
