using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour {
	
	public GameObject player;
	public bool smooth = true;

	Rigidbody2D rb;
	Rigidbody2D playerRb;

	float offset;

	void Start() {
		offset = player.transform.position.x - transform.position.x;

		if (smooth) {
			rb = GetComponent<Rigidbody2D> ();
			playerRb = player.GetComponent<Rigidbody2D> ();

			StartCoroutine ("Smooth");
		}
	}

	IEnumerator Smooth() {
		while (true) {
			Vector2 velocity = rb.velocity;
			velocity.x += playerRb.velocity.x - rb.velocity.x + player.transform.position.x - transform.position.x - offset;
			rb.velocity = velocity;
			yield return new WaitForSeconds (0.025f);
		}
	}

	void Update () {
		if (!smooth) {
			Vector3 position = transform.position;
			position.x = player.transform.position.x - offset;
			transform.position = position;
		}
	}
}
