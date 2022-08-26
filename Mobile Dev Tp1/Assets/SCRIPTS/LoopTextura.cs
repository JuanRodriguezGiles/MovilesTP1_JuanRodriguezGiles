using UnityEngine;
using System.Collections;

public class LoopTextura : MonoBehaviour 
{
	public float Intervalo = 1;
	private float Tempo = 0;
	
	public Texture2D[] Imagenes;
	private int Contador = 0;

	// Use this for initialization
	private void Start () 
	{
		if(Imagenes.Length > 0)
			GetComponent<Renderer>().material.mainTexture = Imagenes[0];
	}
	
	// Update is called once per frame
	private void Update () 
	{
		Tempo += Time.deltaTime;
		
		if(Tempo >= Intervalo)
		{
			Tempo = 0;
			Contador++;
			if(Contador >= Imagenes.Length)
			{
				Contador = 0;
			}
			GetComponent<Renderer>().material.mainTexture = Imagenes[Contador];
		}
	}
}
