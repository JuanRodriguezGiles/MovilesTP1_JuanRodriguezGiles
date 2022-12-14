using UnityEngine;

public class Estanteria : ManejoPallets
{
    public Cinta CintaReceptora; //cinta que debe recibir la bolsa
    public Pallet.Valores Valor;
    public bool Anim;


    //animacion de parpadeo
    public float Intervalo = 0.7f;
    public float Permanencia = 0.2f;
    public GameObject ModelSuelo;
    public Color32 ColorParpadeo;
    private float AnimTempo;
    private Color32 ColorOrigModel;
    private PilaPalletMng Contenido;

    //--------------------------------//	

    private void Start()
    {
        Contenido = GetComponent<PilaPalletMng>();
        ColorOrigModel = ModelSuelo.GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        //animacion de parpadeo
        if (Anim)
        {
            AnimTempo += Time.deltaTime;
            if (AnimTempo > Permanencia)
                if (ModelSuelo.GetComponent<Renderer>().material.color == ColorParpadeo)
                {
                    AnimTempo = 0;
                    ModelSuelo.GetComponent<Renderer>().material.color = ColorOrigModel;
                }

            if (AnimTempo > Intervalo)
                if (ModelSuelo.GetComponent<Renderer>().material.color == ColorOrigModel)
                {
                    AnimTempo = 0;
                    ModelSuelo.GetComponent<Renderer>().material.color = ColorParpadeo;
                }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var recept = other.GetComponent<ManejoPallets>();
        if (recept != null) Dar(recept);
    }

    //------------------------------------------------------------//

    public override void Dar(ManejoPallets receptor)
    {
        if (Tenencia())
            if (Controlador.GetPalletEnMov() == null)
                if (receptor.Recibir(Pallets[0]))
                {
                    //enciende la cinta y el indicador
                    //cambia la textura de cuantos pallet le queda
                    CintaReceptora.Encender();
                    Controlador.SalidaPallet(Pallets[0]);
                    Pallets[0].GetComponent<Renderer>().enabled = true;
                    Pallets.RemoveAt(0);
                    Contenido.Sacar();
                    ApagarAnim();
                }
    }

    public override bool Recibir(Pallet pallet)
    {
        pallet.CintaReceptora = CintaReceptora.gameObject;
        pallet.Portador = gameObject;
        Contenido.Agregar();
        pallet.GetComponent<Renderer>().enabled = false;
        return base.Recibir(pallet);
    }

    public void ApagarAnim()
    {
        Anim = false;
        ModelSuelo.GetComponent<Renderer>().material.color = ColorOrigModel;
    }

    public void EncenderAnim()
    {
        Anim = true;
        ModelSuelo.GetComponent<Renderer>().material.color = ColorOrigModel;
    }
}