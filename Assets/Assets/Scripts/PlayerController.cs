using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float jumpHeight = 4;

	public Animation anim = null;
	Rigidbody2D rb;

	int collideCount = 0;
	bool jumping = false, grounded = true;

	void Start () {
		rb = GetComponentInChildren<Rigidbody2D> ();
		anim = GetComponentInChildren<Animation> ();
	}

	void OnTriggerEnter2D() {
		if (collideCount == 0) {
			grounded = true;
			if (jumping)
				jumping = false;
		}
		collideCount++;
	}

	void OnTriggerExit2D() {
		collideCount--;
		if (collideCount == 0) {
			grounded = false;
			if (!jumping)
				playAnimation ("Fall");
		}
	}

	void Update () {
		if (!GameManager.simulate)
			return;

		if (Input.GetKey(KeyCode.Space))
			tryJump ();
	}

	void tryJump() {
		if (rb.velocity.y == 0) {
			rb.velocity = Vector3.up * jumpHeight;
			playAnimation ("Jump");
			jumping = true;
		}
	}

	void playAnimation(string name) {
		if (anim.IsPlaying (name))
			anim.Stop (name);
		anim.Play (name);
	}
}
