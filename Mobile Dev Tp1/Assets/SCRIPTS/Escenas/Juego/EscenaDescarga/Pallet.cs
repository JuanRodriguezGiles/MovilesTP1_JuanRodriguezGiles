using UnityEngine;

public class Pallet : MonoBehaviour
{
    public enum Valores
    {
        Valor1 = 100000,
        Valor2 = 250000,
        Valor3 = 500000
    }

    public Valores Valor;
    public float Tiempo;
    public GameObject CintaReceptora;
    public GameObject Portador;
    public float TiempEnCinta = 1.5f;
    public float TempoEnCinta;


    public float TiempSmoot = 0.3f;
    public bool EnSmoot;
    private float TempoSmoot;

    //----------------------------------------------//

    private void Start()
    {
        Pasaje();
    }

    private void LateUpdate()
    {
        if (Portador != null)
        {
            if (EnSmoot)
            {
                TempoSmoot += Time.deltaTime;
                if (TempoSmoot >= TiempSmoot)
                {
                    EnSmoot = false;
                    TempoSmoot = 0;
                }
                else
                {
                    if (Portador.GetComponent<ManoRecept>() != null)
                        transform.position = Portador.transform.position - Vector3.up * 1.2f;
                    else
                        transform.position = Vector3.Lerp(transform.position, Portador.transform.position, Time.deltaTime * 10);
                }
            }
            else
            {
                if (Portador.GetComponent<ManoRecept>() != null)
                    transform.position = Portador.transform.position - Vector3.up * 1.2f;
                else
                    transform.position = Portador.transform.position;
            }
        }
    }

    //----------------------------------------------//

    public void Pasaje()
    {
        EnSmoot = true;
        TempoSmoot = 0;
    }
}