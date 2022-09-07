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

    public Estados EstAct = Estados.Calibrando;
    public ManejoPallets Partida;
    public ManejoPallets Llegada;
    public Pallet P;
    public ManejoPallets palletsMover;
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
            SetActivComp(true);
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
        GameManager.Instance.levelController.FinCalibracion(Pj.IdPlayer);
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