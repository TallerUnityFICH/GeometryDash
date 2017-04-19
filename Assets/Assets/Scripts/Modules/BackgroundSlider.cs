using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSlider : MonoBehaviour {

	public float scrollSpeed = 1;
	private Rect savedOffset;

	public static float time = 0;

	void Start () {
		savedOffset = GetComponent<RawImage>().uvRect;

		StartCoroutine ("UpdateSmooth");
	}

	IEnumerator UpdateSmooth() {
		while (true) {
			if (GameManager.simulate) {
				float x = Mathf.Repeat (time * 0.1f * scrollSpeed, 1);
				time += 0.02f;
				Rect offset = savedOffset;
				offset.x = x;
				GetComponent<RawImage> ().uvRect = offset;
			}
			yield return new WaitForSeconds (0.025f);
		}
	}

	void OnDisable () {
		GetComponent<RawImage>().uvRect = savedOffset;
	}
}
