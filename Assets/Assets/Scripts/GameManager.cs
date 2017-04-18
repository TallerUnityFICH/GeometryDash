using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager global;
	public static bool simulate = true;
	public static bool loser;
	public static bool canRestart = false;

	public GameObject background;
	public GameObject floor;

	public GameObject damage;
	public GameObject obstacles;
	public Sprite[] sprite;

	public float animSpeed = 1;

	public Transform mapEnd;

	float lastUpdate = 0;
	SpriteRenderer render;
	SpriteRenderer childRender;
	Transform child;
	Animation childAnim;
	Animation damageAnim;

	PlayerController controller;

	Color alpha;
	Vector3 scale = new Vector3 (0.02f, 0.02f, 0);
	Vector3 rotation = new Vector3 (0, 0, 2);
	float animDelay;
	int animIndex = 0;
	int lifes = 1;

	void Start () {
		global = this;
		Cursor.visible = false;

		simulate = true;
		loser = false;

		animDelay = animSpeed / sprite.Length;

		rotation /= animSpeed*2;
		scale /= animSpeed * 2;

		render = GetComponent<SpriteRenderer> ();
		child = transform.GetChild (0);
		childRender = child.GetComponent<SpriteRenderer> ();
		childAnim = child.GetComponent<Animation> ();
		controller = GetComponent<PlayerController> ();
		damageAnim = damage.transform.GetComponent<Animation> ();

		mapEnd = GameObject.Find ("MapEnd").transform;

		int rand = Random.Range (0, 215);
		setColor (new Color(1 - rand * 3f % 255f / 255, 1 - rand / 255f, 1 - rand * 2f % 255f / 255));

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
			} else if (animIndex == 0) {
				render.sprite = null;
				canRestart = true;
			}
		}
	}

	public void Damage(bool fatallity) {
		damageAnim.Play ("Damage");
		if (!fatallity && lifes > 0) {
			lifes--;
			return;
		}

		lifes = 1;

		GameOver ();
	}

	public void GameOver() {
		controller.showMenu (true);

		simulate = false;
		childAnim.Stop ();
		Cursor.visible = true;

		GetComponent<Rigidbody2D> ().simulated = false;
		child.GetComponent<Collider2D> ().enabled = false;
		animIndex = sprite.Length;

		loser = true;
	}

	public void restart() {
		if (canRestart) {
			global.controller.showMenu (false);
			global.StartCoroutine ("delayReset");
		}
	}

	public void setColor(Color color) {
		background.GetComponent<RawImage> ().color = color;
		floor.GetComponent<FloorGenerator> ().updateColor (new Color ( (1 - color.r) * 1, (1 - color.g) * 2, 1 - color.b));
	}

	IEnumerator delayReset() {
		yield return new WaitForSeconds (1.4f);
		SceneManager.LoadScene (2);
	}
}
