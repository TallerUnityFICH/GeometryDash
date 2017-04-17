using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSlider : MonoBehaviour {

	public float speed = 1;
	public bool ready = false;

	Vector3 step;
	float width;

	void Start () {
		step = new Vector3 (-speed/30, 0, 0);
		width = GameObject.Find("MapEnd").transform.localPosition.x - GetComponent<Collider2D> ().bounds.extents.x + (Screen.width > 720 ? (-Screen.width / 180f + 4) : 0.5f);
	}

	void Update() {
		if (GameManager.simulate && ready) {
			transform.position += step;
			if (transform.localPosition.x < width)
				Destroy (gameObject);
		}
	}
}
