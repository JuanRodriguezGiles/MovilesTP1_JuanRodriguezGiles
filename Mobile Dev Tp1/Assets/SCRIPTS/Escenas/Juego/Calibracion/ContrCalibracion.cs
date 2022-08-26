using UnityEngine;

public class ContrCalibracion : MonoBehaviour
{
    public enum Estados
    {
        Calibrando,
        Tutorial,
        Finalizado
    }

    public Player Pj;

    public float TiempEspCalib = 3;
    public Estados EstAct = Estados.Calibrando;

    public ManejoPallets Partida;
    public ManejoPallets Llegada;
    public Pallet P;
    public ManejoPallets palletsMover;
    private float Tempo2;

    //----------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
        palletsMover.enabled = false;
        Pj.ContrCalib = this;

        P.CintaReceptora = Llegada.gameObject;
        Partida.Recibir(P);

        SetActivComp(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (EstAct == Estados.Calibrando && Pj.Seleccionado) IniciarTesteo();
        if (EstAct == Estados.Tutorial)
            if (Tempo2 < TiempEspCalib)
            {
                Tempo2 += T.GetDT();
                if (Tempo2 > TiempEspCalib) SetActivComp(true);
            }
    }

    //----------------------------------------------------//

    private void IniciarTesteo()
    {
        EstAct = Estados.Tutorial;
        palletsMover.enabled = true;
    }

    public void FinTutorial()
    {
        EstAct = Estados.Finalizado;
        palletsMover.enabled = false;
        GameManager.Instancia.FinCalibracion(Pj.IdPlayer);
    }

    private void SetActivComp(bool estado)
    {
        if (Partida.GetComponent<Renderer>() != null)
            Partida.GetComponent<Renderer>().enabled = estado;
        Partida.GetComponent<Collider>().enabled = estado;
        if (Llegada.GetComponent<Renderer>() != null)
            Llegada.GetComponent<Renderer>().enabled = estado;
        Llegada.GetComponent<Collider>().enabled = estado;
        P.GetComponent<Renderer>().enabled = estado;
    }
}