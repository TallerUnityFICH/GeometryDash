using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float velocity = 5;
	public float jumpHeight = 4;
	public GameObject panel;

	Animation anim = null;
	Rigidbody2D rb;
	PanelFader fader;

	int collideCount = 0;
	bool jumping = false;

	void Start () {
		rb = GetComponentInChildren<Rigidbody2D> ();
		anim = GetComponentInChildren<Animation> ();
		fader = panel.GetComponent<PanelFader> ();
	}

	void OnTriggerEnter2D() {
		if (collideCount == 0) {
			if (jumping)
				jumping = false;
		}
		collideCount++;
	}

	void OnTriggerExit2D() {
		collideCount--;
		if (collideCount == 0) {
			if (!jumping)
				playAnimation ("Fall");
		}
	}

	void Update () {
		if (!GameManager.loser && Input.GetKeyDown (KeyCode.Escape))
			PlayPause ();
		else if (GameManager.loser)
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Return))
				GameManager.global.restart ();
			else if (Input.GetKey (KeyCode.Escape))
				SceneManager.LoadScene (0);
		
		if (!GameManager.simulate)
			return;

		if (rb.velocity.x < velocity)
			rb.velocity += new Vector2 (velocity - rb.velocity.x, 0);

		if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(0))
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

	void PlayPause() {
		if (GameManager.simulate) {
			GameManager.simulate = false;
			Cursor.visible = true;

			showMenu(true);
		} else {
			GameManager.simulate = true;
			Cursor.visible = false;

			showMenu(false);
		}
	}

	public void showMenu(bool show) {
		if (show)
			fader.show ();
		else
			fader.hide ();
	}
}
