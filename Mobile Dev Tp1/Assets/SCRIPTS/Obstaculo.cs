using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public float ReduccionVel;
    public float TiempEmpDesapa = 1;
    public float TiempDesapareciendo = 1;
    public string PlayerTag = "Player";

    private bool Chocado;
    private bool Desapareciendo;
    private float Tempo1;
    private float Tempo2;

    // Update is called once per frame
    private void Update()
    {
        if (Chocado)
        {
            Tempo1 += T.GetDT();
            if (Tempo1 > TiempEmpDesapa)
            {
                Chocado = false;
                Desapareciendo = true;
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Collider>().enabled = false;
            }
        }

        if (Desapareciendo)
        {
            //animacion de desaparecer

            Tempo2 += T.GetDT();
            if (Tempo2 > TiempDesapareciendo) gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == PlayerTag) Chocado = true;
    }

    //------------------------------------------------//

    protected virtual void Desaparecer()
    {
    }

    protected virtual void Colision()
    {
    }
}