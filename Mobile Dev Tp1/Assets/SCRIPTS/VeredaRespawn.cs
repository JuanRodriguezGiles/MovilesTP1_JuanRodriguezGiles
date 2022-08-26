using UnityEngine;
using System.Collections;

public class VeredaRespawn : MonoBehaviour 
{
	public string PlayerTag = "Player";

	// Use this for initialization
	private void Start () 
	{
		GetComponent<Renderer>().enabled = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == PlayerTag)
		{
			other.GetComponent<Respawn>().Respawnear();
		}	
	}

	private void OnCollisionEnter(Collision collision) 
	{
		if(collision.gameObject.tag == PlayerTag)
		{
			collision.gameObject.GetComponent<Respawn>().Respawnear();
		}
	}
	
}
