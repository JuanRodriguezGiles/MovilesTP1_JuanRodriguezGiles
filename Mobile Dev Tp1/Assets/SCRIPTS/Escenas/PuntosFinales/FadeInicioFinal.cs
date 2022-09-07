using UnityEngine;

public class FadeInicioFinal : MonoBehaviour
{
    [SerializeField] private EndScreenController Mng;
    public float Duracion = 2;

    private Color aux;
    private bool MngAvisado;
    private float TiempInicial;

    private void Start()
    {
        TiempInicial = Mng.TiempEspReiniciar;

        aux = GetComponent<Renderer>().material.color;
        aux.a = 0;
        GetComponent<Renderer>().material.color = aux;
    }

    private void Update()
    {
        if (Mng.TiempEspReiniciar > TiempInicial - Duracion) //aparicion
        {
            aux = GetComponent<Renderer>().material.color;
            aux.a += Time.deltaTime / Duracion;
            GetComponent<Renderer>().material.color = aux;
        }
        else if (Mng.TiempEspReiniciar < Duracion) //desaparicion
        {
            aux = GetComponent<Renderer>().material.color;
            aux.a -= Time.deltaTime / Duracion;
            GetComponent<Renderer>().material.color = aux;

            if (!MngAvisado)
            {
                MngAvisado = true;
                Mng.DesaparecerGUI();
            }
        }
    }
}