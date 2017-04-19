using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelFader : MonoBehaviour {

	Animation anim;

	void Start() {
		anim = GetComponent<Animation> ();
	}

	public void show() {
		if (!FeaturesEnabler.menu) {
			transform.GetChild (0).localPosition = Vector3.zero;

			Image img = GetComponent<Image> ();
			Color color = img.color;
			color.a = 100f / 255;
			img.color = color;

			GetComponentInChildren<ReanudarButton> ().shouldCheck = true;
		} else
			playAnimation ("PauseFadeIN");
	}

	public void hide() {
		if (!FeaturesEnabler.menu) {
			transform.GetChild (0).localPosition = new Vector3 (0, -600, 0);

			Image img = GetComponent<Image> ();
			Color color = img.color;
			color.a = 0f;
			img.color = color;
		} else
			playAnimation ("PauseFadeOUT");
	}

	void playAnimation(string name) {
		if (anim.IsPlaying (name))
			anim.CrossFade (name);
		else
			anim.Play (name);
	}
}
