using UnityEngine;
using System.Collections;

public class AcelerAuto : MonoBehaviour 
{
	public float AcelPorSeg = 0;
	private float Velocidad = 0;
	public float VelMax = 0;
	private ReductorVelColl Obstaculo = null;


	private bool Avil = true;
	public float TiempRecColl = 0;
	private float Tempo = 0;

	// Use this for initialization
	private void Start () 
	{
	
	}
	
	// Update is called once per frame
	private void Update ()
	{
		/*
		if(Velocidad < VelMax)
		{
			Velocidad += AcelPorSeg * Time.deltaTime;
		}
		*/
		
		//Debug.Log("Velocidad: "+rigidbody.velocity.magnitude);
		
		if(Avil)
		{
			Tempo += Time.deltaTime;
			if(Tempo > TiempRecColl)
			{
				Tempo = 0;
				Avil = false;
			}
		}
	}

	private void FixedUpdate () 
	{
		/*
		//this.rigidbody.MovePosition(this.transform.position + this.transform.forward * Velocidad);
		if(rigidbody.velocity.magnitude < VelMax)
			rigidbody.velocity += transform.forward * AcelPorSeg * Time.deltaTime;
			*/
		
		
		/*
		if(Velocidad < VelMax)
		{
			Velocidad += AcelPorSeg * Time.fixedDeltaTime;
		}
		
		rigidbody.MovePosition(this.transform.position + this.transform.forward * Velocidad);
		*/
		
		if(Velocidad < VelMax)
		{
			Velocidad += AcelPorSeg * Time.fixedDeltaTime;
		}
		
		GetComponent<Rigidbody>().AddForce(this.transform.forward * Velocidad);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(!Avil)
		{
			Obstaculo = collision.transform.GetComponent<ReductorVelColl>();
			if(Obstaculo != null)
			{
				
				//Velocidad -= Obstaculo.ReduccionVel;
				
				//if(Velocidad < 0)
					//Velocidad = 0;
					
				GetComponent<Rigidbody>().velocity /= 2;
			}
			Obstaculo = null;
		}
	}
	
	public void Chocar(ReductorVelColl obst)
	{
		GetComponent<Rigidbody>().velocity /= 2;
	}
	
}
