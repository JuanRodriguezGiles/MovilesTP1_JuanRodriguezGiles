using UnityEngine;

public class Frenado : MonoBehaviour 
{
	public float VelEntrada = 0;
	public string TagDeposito = "Deposito";

	private int Contador = 0;
	private int CantMensajes = 10;
	private float TiempFrenado = 0.5f;
	private float Tempo = 0f;

	private Vector3 Destino;
	
	public bool Frenando = false;
	
	//-----------------------------------------------------//
	
	// Use this for initialization
	private void Start () 
	{
		Frenar();
	}

	private void FixedUpdate ()
	{
		if(Frenando)
		{
			Tempo += T.GetFDT();
			if(Tempo >= (TiempFrenado / CantMensajes) * Contador)
			{
				Contador++;
			}
		}
	}

	private void OnTriggerEnter(Collider other) 
	{
		if(other.tag == TagDeposito)
		{
			Deposito2 dep = other.GetComponent<Deposito2>();
			if(dep.Vacio)
			{	
				if(this.GetComponent<Player>().ConBolasas())
				{
					dep.Entrar(this.GetComponent<Player>());
					Destino = other.transform.position;
					transform.forward = Destino - transform.position;
					Frenar();
				}				
			}
		}
	}
	
	//-----------------------------------------------------------//
	
	public void Frenar()
	{
		GetComponent<ControlDireccion>().enabled = false;
		gameObject.GetComponent<CarController>().SetAcel(0f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
		
		Frenando = true;
		Tempo = 0;
		Contador = 0;
	}
	
	public void RestaurarVel()
	{
		GetComponent<ControlDireccion>().enabled = true;
		gameObject.GetComponent<CarController>().SetAcel(1f);
        Frenando = false;
		Tempo = 0;
		Contador = 0;
	}
}
