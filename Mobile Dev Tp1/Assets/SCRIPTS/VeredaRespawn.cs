using UnityEngine;

public class VeredaRespawn : MonoBehaviour
{
    public string PlayerTag = "Player";

    // Use this for initialization
    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == PlayerTag) collision.gameObject.GetComponent<Respawn>().Respawnear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PlayerTag) other.GetComponent<Respawn>().Respawnear();
    }
}