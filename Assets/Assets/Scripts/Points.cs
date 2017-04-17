using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Text))]
public class Points : MonoBehaviour {
	private Text m_Text;
	double aux;
	private void Start()
	{
		m_Text = GetComponent<Text>();
	}

	private void Update()
	{
		if (GameManager.loser==false)
		{
			aux = Math.Truncate (10 * Time.time);
			m_Text.text = "Puntaje: " + aux.ToString ();
		}
	}
}
