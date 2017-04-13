using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float jumpHeight = 4;

	void Start () {
		
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
			tryJump ();
	}

	void tryJump() {
		GetComponent<Rigidbody2D> ().velocity = Vector3.up * jumpHeight;
	}
}
