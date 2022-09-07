using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Estados
    {
        EnDescarga,
        EnConduccion,
        EnTutorial,
        Ninguno
    }

    public int Dinero;
    public int IdPlayer;

    public Bolsa[] bolsas;
    public string TagBolsas = "";
    public Estados EstAct = Estados.EnConduccion;

    public bool EnConduccion = true;
    public bool EnDescarga;

    public ControladorDeDescarga ContrDesc;
    public ContrCalibracion ContrCalib;

    public bool Seleccionado;
    public bool FinCalibrado;
    public bool FinTuto;
    private int CantBolsAct;

    public Visualizacion MiVisualizacion;

    public Visualizacion.Lado LadoActual => MiVisualizacion.LadoAct;

    //------------------------------------------------------------------//

    private void Start()
    {
        for (var i = 0; i < bolsas.Length; i++)
            bolsas[i] = null;

        MiVisualizacion = GetComponent<Visualizacion>();
    }

    //------------------------------------------------------------------//

    public bool AgregarBolsa(Bolsa b)
    {
        if (CantBolsAct + 1 <= bolsas.Length)
        {
            bolsas[CantBolsAct] = b;
            CantBolsAct++;
            Dinero += (int)b.Monto;
            b.Desaparecer();
            return true;
        }

        return false;
    }

    public void VaciarInv()
    {
        for (var i = 0; i < bolsas.Length; i++)
            bolsas[i] = null;

        CantBolsAct = 0;
    }

    public bool ConBolasas()
    {
        for (var i = 0; i < bolsas.Length; i++)
            if (bolsas[i] != null)
                return true;
        return false;
    }

    public void SetContrDesc(ControladorDeDescarga contr)
    {
        ContrDesc = contr;
    }

    public ControladorDeDescarga GetContr()
    {
        return ContrDesc;
    }

    public void CambiarATutorial()
    {
        EstAct = Estados.EnTutorial;
        MiVisualizacion.CambiarATutorial();
    }

    public void CambiarAConduccion()
    {
        EstAct = Estados.EnConduccion;
        MiVisualizacion.CambiarAConduccion();
    }

    public void CambiarADescarga()
    {
        EstAct = Estados.EnDescarga;
        MiVisualizacion.CambiarADescarga();
    }

    public void SacarBolasa()
    {
        for (var i = 0; i < bolsas.Length; i++)
            if (bolsas[i] != null)
            {
                bolsas[i] = null;
                return;
            }
    }
}