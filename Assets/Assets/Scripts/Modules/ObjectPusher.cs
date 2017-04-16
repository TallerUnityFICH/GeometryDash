using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPusher : MonoBehaviour {

	public float velocity;

	float waitTime;
	Vector3 step;

	void Start () {
		waitTime = 1 / (velocity > 0 ? velocity : -velocity) / 60f;
		step = new Vector3 (1/12f * (velocity == 0 ? 0 : velocity > 0 ? 1 : -1), 0, 0);

		StartCoroutine ("Move");
	}

	IEnumerator Move() {
		while (GameManager.simulate) {
			transform.position += step;

			yield return new WaitForSeconds (waitTime);
		}
	}
}
