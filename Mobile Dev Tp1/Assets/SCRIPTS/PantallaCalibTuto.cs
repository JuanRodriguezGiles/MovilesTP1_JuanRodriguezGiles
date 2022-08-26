using UnityEngine;
using System.Collections;

public class PantallaCalibTuto : MonoBehaviour 
{
	public Texture2D[] ImagenesDelTuto;
	public float Intervalo = 1.2f;//tiempo de cada cuanto cambia de imagen
	private float TempoIntTuto = 0;
	private int EnCursoTuto = 0;
	
	public Texture2D[] ImagenesDeCalib;
	private int EnCursoCalib = 0;
	private float TempoIntCalib = 0;
	
	public Texture2D ImaReady;
	
	public ContrCalibracion ContrCalib;
	
	// Update is called once per frame
	private void Update () 
	{
		switch(ContrCalib.EstAct)
		{
		case ContrCalibracion.Estados.Calibrando:
			//pongase en posicion para iniciar
			TempoIntCalib += T.GetDT();
			if(TempoIntCalib >= Intervalo)
			{
				TempoIntCalib = 0;
				if(EnCursoCalib + 1 < ImagenesDeCalib.Length)
					EnCursoCalib++;
				else
					EnCursoCalib = 0;
			}
			GetComponent<Renderer>().material.mainTexture = ImagenesDeCalib[EnCursoCalib];
			
			break;
			
		case ContrCalibracion.Estados.Tutorial:
			//tome la bolsa y depositela en el estante
			TempoIntTuto += T.GetDT();
			if(TempoIntTuto >= Intervalo)
			{
				TempoIntTuto = 0;
				if(EnCursoTuto + 1 < ImagenesDelTuto.Length)
					EnCursoTuto++;
				else
					EnCursoTuto = 0;
			}
			GetComponent<Renderer>().material.mainTexture = ImagenesDelTuto[EnCursoTuto];
			
			break;
			
		case ContrCalibracion.Estados.Finalizado:
			//esperando al otro jugador		
			GetComponent<Renderer>().material.mainTexture = ImaReady;
			
			break;
		}
			
			
	}
}
