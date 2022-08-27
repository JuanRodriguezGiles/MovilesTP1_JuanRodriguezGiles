using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public string PlayerTag = "Player";
    public float TiempPermanencia = 0.7f; //tiempo que no deja respaunear a un pj desp que el otro lo hizo.
    private bool HabilitadoResp = true;
    private float Tempo;

    // Use this for initialization
    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!HabilitadoResp)
        {
            Tempo += T.GetDT();
            if (Tempo >= TiempPermanencia)
            {
                Tempo = 0;
                HabilitadoResp = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PlayerTag) other.GetComponent<Respawn>().AgregarCP(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == PlayerTag) HabilitadoResp = true;
    }

    //---------------------------------------------------//

    public bool Habilitado()
    {
        if (HabilitadoResp)
        {
            HabilitadoResp = false;
            Tempo = 0;
            return true;
        }

        return HabilitadoResp;
    }
}