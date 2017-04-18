using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDestroyer : MonoBehaviour {
	
	float offsetX;

	void Start () {
		offsetX = GetComponent<SpriteRenderer> ().bounds.extents.x;
	}

	void Update () {
		if (transform.position.x + offsetX < GameManager.global.mapEnd.position.x - 2)
			transform.parent.GetComponent<FloorGenerator> ().DisableFloor (gameObject);
	}
}
