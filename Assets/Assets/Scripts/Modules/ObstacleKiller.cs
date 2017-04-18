using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleKiller : MonoBehaviour {
	float width;

	void Start () {
		width = GetComponent<Collider2D> ().bounds.extents.x + (Screen.width > 720 ? (-Screen.width / 180f + 4) : 0.5f);
	}

	void Update() {
		if (transform.localPosition.x < GameManager.global.mapEnd.position.x + width)
			Destroy (gameObject);
	}
}
