using System.Collections.Generic;

using UnityEngine;

public class ControladorDeDescarga : MonoBehaviour
{
    public GameObject[] Componentes; //todos los componentes que debe activar en esta escena

    public Player Pj; //jugador

    public Pallet PEnMov;

    //las camaras que enciende y apaga
    public GameObject CamaraConduccion;
    public GameObject CamaraDescarga;

    //los prefab de los pallets
    public GameObject Pallet1;
    public GameObject Pallet2;
    public GameObject Pallet3;


    public Estanteria Est1;
    public Estanteria Est2;
    public Estanteria Est3;

    public Cinta Cin2;

    public float Bonus;


    public AnimMngDesc ObjAnimado;
    private MeshCollider CollCamion;

    private int Contador;

    private Deposito2 Dep;
    private List<Pallet.Valores> Ps = new List<Pallet.Valores>();
    private float TempoBonus;


    //--------------------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
        for (var i = 0; i < Componentes.Length; i++) Componentes[i].SetActive(false);

        CollCamion = Pj.GetComponentInChildren<MeshCollider>();
        Pj.SetContrDesc(this);
        if (ObjAnimado != null)
            ObjAnimado.ContrDesc = this;
    }

    // Update is called once per frame
    private void Update()
    {
        //contador de tiempo
        if (PEnMov != null)
        {
            if (TempoBonus > 0)
            {
                Bonus = TempoBonus * (float)PEnMov.Valor / PEnMov.Tiempo;
                TempoBonus -= T.GetDT();
            }
            else
            {
                Bonus = 0;
            }
        }
    }

    //--------------------------------------------------------------//

    public void Activar(Deposito2 d)
    {
        Dep = d; //recibe el deposito para que sepa cuando dejarlo ir al camion
        CamaraConduccion.SetActive(false); //apaga la camara de conduccion

        //activa los componentes
        for (var i = 0; i < Componentes.Length; i++) Componentes[i].SetActive(true);


        CollCamion.enabled = false;
        Pj.CambiarADescarga();


        GameObject go;
        //asigna los pallets a las estanterias
        for (var i = 0; i < Pj.Bolasas.Length; i++)
            if (Pj.Bolasas[i] != null)
            {
                Contador++;

                switch (Pj.Bolasas[i].Monto)
                {
                    case Pallet.Valores.Valor1:
                        go = Instantiate(Pallet1);
                        Est1.Recibir(go.GetComponent<Pallet>());
                        break;

                    case Pallet.Valores.Valor2:
                        go = Instantiate(Pallet2);
                        Est2.Recibir(go.GetComponent<Pallet>());
                        break;

                    case Pallet.Valores.Valor3:
                        go = Instantiate(Pallet3);
                        Est3.Recibir(go.GetComponent<Pallet>());
                        break;
                }
            }

        //animacion
        ObjAnimado.Entrar();
    }

    //cuando sale de un estante
    public void SalidaPallet(Pallet p)
    {
        PEnMov = p;
        TempoBonus = p.Tiempo;
        Pj.SacarBolasa();
        //inicia el contador de tiempo para el bonus
    }

    //cuando llega a la cinta
    public void LlegadaPallet(Pallet p)
    {
        //termina el contador y suma los pts

        //termina la descarga
        PEnMov = null;
        Contador--;

        Pj.Dinero += (int)Bonus;

        if (Contador <= 0)
            Finalizacion();
        else
            Est2.EncenderAnim();
    }

    //metodo llamado por el GameManager para avisar que se termino el juego
    public void FinDelJuego()
    {
        //desactiva lo que da y recibe las bolsas para que no halla mas flujo de estas
        Est2.enabled = false;
        Cin2.enabled = false;
    }

    private void Finalizacion()
    {
        ObjAnimado.Salir();
    }

    public Pallet GetPalletEnMov()
    {
        return PEnMov;
    }

    public void FinAnimEntrada()
    {
        //avisa cuando termino la animacion para que prosiga el juego
        Est2.EncenderAnim();
    }

    public void FinAnimSalida()
    {
        //avisa cuando termino la animacion para que prosiga el juego

        for (var i = 0; i < Componentes.Length; i++) Componentes[i].SetActive(false);

        CamaraConduccion.SetActive(true);

        CollCamion.enabled = true;

        Pj.CambiarAConduccion();

        Dep.Soltar();
    }
}