using UnityEngine;
using System.Collections;

public class DisableObjects : MonoBehaviour
{
	public GameObject theObject;
	
	private Renderer[] renders = null;

	private void Start()
	{
		Component[] comps = theObject.transform.GetComponentsInChildren(typeof(Renderer));
		renders = new Renderer[comps.Length];
		for(int i = 0; i < comps.Length; i++)
			renders[i] = comps[i] as Renderer;
		if(renders == null)
			renders = new Renderer[0];
	}

	private void OnTriggerEnter()
	{
		foreach(Renderer rend in renders)
			rend.enabled = false;
	}

	private void OnTriggerExit()
	{
		foreach(Renderer rend in renders)
			rend.enabled = true;
	}
}
