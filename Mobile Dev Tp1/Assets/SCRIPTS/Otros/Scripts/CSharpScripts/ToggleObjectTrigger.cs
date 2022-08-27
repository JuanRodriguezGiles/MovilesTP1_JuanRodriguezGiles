using UnityEngine;

public class ToggleObjectTrigger : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Renderer>().enabled = false;
    }

    private void OnTriggerEnter()
    {
        GetComponent<Renderer>().enabled = true;
    }

    private void OnTriggerExit()
    {
        GetComponent<Renderer>().enabled = false;
    }
}