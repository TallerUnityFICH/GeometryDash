using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFader : MonoBehaviour {

	Animation anim;

	void Start() {
		anim = GetComponent<Animation> ();
	}

	public void show() {
		playAnimation ("PauseFadeIN");
	}

	public void hide() {
		playAnimation ("PauseFadeOUT");
	}

	void playAnimation(string name) {
		if (anim.IsPlaying (name))
			anim.CrossFade (name);
		else
			anim.Play (name);
	}
}
