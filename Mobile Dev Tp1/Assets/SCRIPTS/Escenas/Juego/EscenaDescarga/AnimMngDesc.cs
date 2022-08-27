using UnityEngine;

public class AnimMngDesc : MonoBehaviour
{
    public string AnimEntrada = "Entrada";
    public string AnimSalida = "Salida";
    public ControladorDeDescarga ContrDesc;

    public GameObject PuertaAnimada;

    private AnimEnCurso AnimAct = AnimEnCurso.Nada;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            Entrar();
        if (Input.GetKeyDown(KeyCode.X))
            Salir();

        switch (AnimAct)
        {
            case AnimEnCurso.Entrada:

                if (!GetComponent<Animation>().IsPlaying(AnimEntrada))
                {
                    AnimAct = AnimEnCurso.Nada;
                    ContrDesc.FinAnimEntrada();
                }

                break;

            case AnimEnCurso.Salida:

                if (!GetComponent<Animation>().IsPlaying(AnimSalida))
                {
                    AnimAct = AnimEnCurso.Nada;
                    ContrDesc.FinAnimSalida();
                }

                break;

            case AnimEnCurso.Nada:
                break;
        }
    }

    public void Entrar()
    {
        AnimAct = AnimEnCurso.Entrada;
        GetComponent<Animation>().Play(AnimEntrada);

        if (PuertaAnimada != null)
        {
            PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].time = 0;
            PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].speed = 1;
            PuertaAnimada.GetComponent<Animation>().Play("AnimPuerta");
        }
    }

    public void Salir()
    {
        AnimAct = AnimEnCurso.Salida;
        GetComponent<Animation>().Play(AnimSalida);

        if (PuertaAnimada != null)
        {
            PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].time = PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].length;
            PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].speed = -1;
            PuertaAnimada.GetComponent<Animation>().Play("AnimPuerta");
        }
    }

    private enum AnimEnCurso
    {
        Salida,
        Entrada,
        Nada
    }
}