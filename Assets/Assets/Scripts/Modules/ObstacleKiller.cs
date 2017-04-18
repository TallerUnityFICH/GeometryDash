using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleKiller : MonoBehaviour {
	
	public float speed = 1;

	Transform mapEnd;
	float width;

	void Start () {
		mapEnd = GameObject.Find ("MapEnd").transform;
		width = GetComponent<Collider2D> ().bounds.extents.x + (Screen.width > 720 ? (-Screen.width / 180f + 4) : 0.5f);
	}

	void Update() {
		if (transform.localPosition.x < mapEnd.localPosition.x - width)
			Destroy (gameObject);
	}
}
