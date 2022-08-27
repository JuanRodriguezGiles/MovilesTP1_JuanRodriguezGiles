using UnityEngine;

public class CollContraObst : MonoBehaviour
{
    public float TiempEsp = 1;
    public float TiempNoColl = 2;

    private Colisiones Colisiono = Colisiones.ConTodo;
    private float Tempo1;
    private float Tempo2;

    // Use this for initialization
    private void Start()
    {
        Physics.IgnoreLayerCollision(8, 10, false);
    }

    // Update is called once per frame
    private void Update()
    {
        switch (Colisiono)
        {
            case Colisiones.ConTodo:
                break;

            case Colisiones.EspDesact:
                Tempo1 += Time.deltaTime;
                if (Tempo1 >= TiempEsp)
                {
                    Tempo1 = 0;
                    IgnorarColls(true);
                }

                break;

            case Colisiones.SinObst:
                Tempo2 += Time.deltaTime;
                if (Tempo2 >= TiempNoColl)
                {
                    Tempo2 = 0;
                    IgnorarColls(false);
                }

                break;
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Obstaculo") ColisionConObst();
    }

    //-------------------------//

    private void ColisionConObst()
    {
        switch (Colisiono)
        {
            case Colisiones.ConTodo:
                Colisiono = Colisiones.EspDesact;
                break;

            case Colisiones.EspDesact:
                break;

            case Colisiones.SinObst:
                break;
        }
    }

    private void IgnorarColls(bool b)
    {
        if (name == "Camion1")
            Physics.IgnoreLayerCollision(8, 10, b);
        else
            Physics.IgnoreLayerCollision(9, 10, b);

        if (b)
            Colisiono = Colisiones.SinObst;
        else
            Colisiono = Colisiones.ConTodo;
    }

    private enum Colisiones
    {
        ConTodo,
        EspDesact,
        SinObst
    }
}