using UnityEngine;

public class Frenado : MonoBehaviour
{
    public float VelEntrada;
    public string TagDeposito = "Deposito";

    public bool Frenando;
    private readonly int CantMensajes = 10;

    private int Contador;

    private Vector3 Destino;
    private float Tempo;
    private readonly float TiempFrenado = 0.5f;

    //-----------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
        Frenar();
    }

    private void FixedUpdate()
    {
        if (Frenando)
        {
            Tempo += Time.fixedDeltaTime;
            if (Tempo >= TiempFrenado / CantMensajes * Contador) Contador++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagDeposito)
        {
            var dep = other.GetComponent<Deposito2>();
            if (dep.Vacio)
                if (GetComponent<Player>().ConBolasas())
                {
                    dep.Entrar(GetComponent<Player>());
                    Destino = other.transform.position;
                    transform.forward = Destino - transform.position;
                    Frenar();
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