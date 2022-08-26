using UnityEngine;
using System.Collections;

public class FadeInicioFinal : MonoBehaviour 
{
	public float Duracion = 2;
	public float Vel = 2;
	private float TiempInicial;

	private MngPts Mng;

	private Color aux;

	private bool MngAvisado = false;

	// Use this for initialization
	private void Start ()
	{
		Mng = (MngPts)GameObject.FindObjectOfType(typeof (MngPts));
		TiempInicial = Mng.TiempEspReiniciar;
		
		aux = GetComponent<Renderer>().material.color;
		aux.a = 0;
		GetComponent<Renderer>().material.color = aux;
	}
	
	// Update is called once per frame
	private void Update () 
	{
		
		if(Mng.TiempEspReiniciar > TiempInicial - Duracion)//aparicion
		{
			aux = GetComponent<Renderer>().material.color;
			aux.a += Time.deltaTime / Duracion;
			GetComponent<Renderer>().material.color = aux;			
		}
		else if(Mng.TiempEspReiniciar < Duracion)//desaparicion
		{
			aux = GetComponent<Renderer>().material.color;
			aux.a -= Time.deltaTime / Duracion;
			GetComponent<Renderer>().material.color = aux;
			
			if(!MngAvisado)
			{
				MngAvisado = true;
				Mng.DesaparecerGUI();
			}
		}
				
	}
}
