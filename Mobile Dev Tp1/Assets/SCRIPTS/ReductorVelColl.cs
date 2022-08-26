using UnityEngine;
using System.Collections;

public class ReductorVelColl : MonoBehaviour 
{
	public float ReduccionVel;
	private bool Usado = false;
	public string PlayerTag = "Player";

	private void OnCollisionEnter(Collision other) 
	{
		if(other.transform.tag == PlayerTag)
		{
			if(!Usado)
			{
				Chocado();
			}
		}
	}
	
	public virtual void Chocado()
	{
		Usado = true;
	}
}
