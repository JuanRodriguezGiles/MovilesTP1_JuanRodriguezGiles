using UnityEngine;
using System.Collections;

public class CopyMove : MonoBehaviour
{
	public Transform Target;
	
	// Update is called once per frame
	private void LateUpdate () 
	{
		transform.position = Target.position;
	}
}
