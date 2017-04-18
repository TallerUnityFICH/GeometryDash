using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Text))]
public class TimePoints : MonoBehaviour {
	private Text m_Text;
	double aux;
	int cont;

	private void Start() {
		m_Text = GetComponent<Text>();
		cont = 0;
		aux = 0;
	}

	private void Update() {
		if (GameManager.simulate) {
			if (aux < 1 || cont > 9)  {
				m_Text.text = "Puntaje: " + aux.ToString ();

				aux++;
				cont=0;
			} else
				cont++;
		}
	}
}
