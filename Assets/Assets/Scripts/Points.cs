using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Text))]
public class Points : MonoBehaviour {
	private Text m_Text;
	double aux;
	int cont;
	private void Start()
	{
		aux = 0;
		m_Text = GetComponent<Text>();
		cont = 0;
	}

	private void Update()
	{
		if (GameManager.loser==false)
		{
			if (cont == 10) 
			{
				aux = aux + 1;
				cont=0;
			} 
			else 
			{
				cont++;
			}
			m_Text.text = "Puntaje: " + aux.ToString ();
		}
	}
}
