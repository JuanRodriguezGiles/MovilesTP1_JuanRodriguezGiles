using UnityEngine;

public class Bolsa : MonoBehaviour
{
    public Pallet.Valores Monto;

    //public int IdPlayer = 0;
    public string TagPlayer = "";
    public Texture2D ImagenInventario;
    public GameObject Particulas;
    public float TiempParts = 2.5f;

    private bool Desapareciendo;
    private Player Pj;

    // Use this for initialization
    private void Start()
    {
        Monto = Pallet.Valores.Valor2;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Desapareciendo)
        {
            TiempParts -= Time.deltaTime;
            if (TiempParts <= 0)
            {
                GetComponent<Renderer>().enabled = true;
                GetComponent<Collider>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == TagPlayer)
        {
            Pj = coll.GetComponent<Player>();
            if (Pj.AgregarBolsa(this))
                Desaparecer();
        }
    }

    public void Desaparecer()
    {
        Particulas.SetActive(true);
        Desapareciendo = true;

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}