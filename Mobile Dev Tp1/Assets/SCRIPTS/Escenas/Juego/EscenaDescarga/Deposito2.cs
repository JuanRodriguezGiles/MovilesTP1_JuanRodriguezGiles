using UnityEngine;

public class Deposito2 : MonoBehaviour
{
    public string PlayerTag = "Player";
    public bool Vacio = true;
    public ControladorDeDescarga Contr1;
    public ControladorDeDescarga Contr2;
    private Player PjActual;

    private Collider[] PjColl;

    //----------------------------------------------//

    private void Start()
    {
        Contr1 = GameObject.Find("ContrDesc1").GetComponent<ControladorDeDescarga>();
        Contr2 = GameObject.Find("ContrDesc2").GetComponent<ControladorDeDescarga>();

        Physics.IgnoreLayerCollision(8, 9, false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Vacio)
        {
            PjActual.transform.position = transform.position;
            PjActual.transform.forward = transform.forward;
        }
    }

    //----------------------------------------------//

    public void Soltar()
    {
        PjActual.VaciarInv();
        PjActual.GetComponent<Frenado>().RestaurarVel();
        PjActual.GetComponent<Respawn>().Respawnear(transform.position, transform.forward);

        PjActual.GetComponent<Rigidbody>().useGravity = true;
        for (var i = 0; i < PjColl.Length; i++)
            PjColl[i].enabled = true;

        Physics.IgnoreLayerCollision(8, 9, false);

        PjActual = null;
        Vacio = true;
    }

    public void Entrar(Player pj)
    {
        if (pj.ConBolasas())
        {
            PjActual = pj;

            PjColl = PjActual.GetComponentsInChildren<Collider>();
            for (var i = 0; i < PjColl.Length; i++)
                PjColl[i].enabled = false;
            PjActual.GetComponent<Rigidbody>().useGravity = false;

            PjActual.transform.position = transform.position;
            PjActual.transform.forward = transform.forward;

            Vacio = false;

            Physics.IgnoreLayerCollision(8, 9, true);

            Entro();
        }
    }

    public void Entro()
    {
        if (PjActual.IdPlayer == 0)
            Contr1.Activar(this);
        else
            Contr2.Activar(this);
    }
}