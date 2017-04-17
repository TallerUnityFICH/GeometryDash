using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager global;
	public static bool simulate = true;

	public GameObject obstacles;
	public Sprite[] sprite;

	public float animSpeed = 1;

	float lastUpdate = 0;
	SpriteRenderer render;
	SpriteRenderer childRender;
	Transform child;

	Color alpha;
	Vector3 scale = new Vector3 (0.02f, 0.02f, 0);
	Vector3 rotation = new Vector3 (0, 0, 2);
	float animDelay;
	int animIndex = 0;
	int lifes = 1;

	void Start () {
		global = this;
		Cursor.visible = false;

		animDelay = animSpeed / sprite.Length;

		rotation /= animSpeed*2;
		scale /= animSpeed * 2;

		render = GetComponent<SpriteRenderer> ();
		child = transform.GetChild (0);
		childRender = child.GetComponent<SpriteRenderer> ();

		alpha = childRender.color;
	}

	void Update () {
		if (animIndex > 0) {
			transform.Rotate (rotation);
			transform.localScale += scale;
			if (lastUpdate == 0 || Time.time - lastUpdate > animDelay) {
				render.sprite = sprite [sprite.Length - animIndex];
				animIndex--;

				lastUpdate = Time.time;

				if (sprite.Length - animIndex == 4)
					childRender.enabled = false;
			}

			if (sprite.Length - animIndex < 4) {
				alpha.a -= 0.04f / animSpeed;
				childRender.color = alpha;
				child.localScale -= scale * 2;
			} else if (animIndex == 0)
				render.sprite = null;
		}
	}

	public void Damage(bool fatallity) {
		if (!fatallity && lifes > 0) {
			lifes--;
			return;
		}

		lifes = 2;

		GameOver ();
	}

	public void GameOver() {
		simulate = false;

		Animation anim = GetComponent<PlayerController> ().anim;
		if (anim.isPlaying)
			anim.Stop ();
		Cursor.visible = true;

		GetComponent<Rigidbody2D> ().simulated = false;
		child.GetComponent<Collider2D> ().enabled = false;
		animIndex = sprite.Length;
		
		print ("GameOver");

		StartCoroutine ("ToMenu");
	}

	IEnumerator ToMenu() {
	yield return new WaitForSeconds (0.75f);
		SceneManager.LoadScene (0);
		simulate = true;
	}
}
