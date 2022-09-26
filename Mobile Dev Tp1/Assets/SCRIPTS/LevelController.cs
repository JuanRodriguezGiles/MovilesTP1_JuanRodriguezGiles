using System;

using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    public Player Player1;
    public Player Player2;
    public Text conteoInicioText;
    public Text tiempoDeJuegoText;

    public Vector3[] PosCamionesCarrera = new Vector3[2];
    public GameObject[] ObjsCalibracion1;
    public GameObject[] ObjsCalibracion2;
    public GameObject[] ObjsCarrera;

    public Transform[] obstacles;
    public GameObject normalOffloadStations;
    public GameObject hardOffloadStations;
    public GameObject normalMoneyBags;
    public GameObject hardMoneyBags;
    public RectTransform bonusUi;

    private int players;
    #endregion

    private void Start()
    {
        players = GameManager.Instance.players;

        Player1.Init();
        Player2.Init();
        
        if (players == 1) 
        {
            SinglePlayerSetup();
        }
        
        IniciarTutorial();
    }
    
    public void IniciarTutorial()
    {
        for (var i = 0; i < ObjsCalibracion1.Length; i++)
        {
            ObjsCalibracion1[i].SetActive(true);
            if (players != 1)
            {
                ObjsCalibracion2[i].SetActive(true);
            }
        }

        for (var i = 0; i < ObjsCarrera.Length; i++) ObjsCarrera[i].SetActive(false);

        Player1.CambiarATutorial();
        if (players != 1)
        {
            Player2.CambiarATutorial();
        }

        tiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        conteoInicioText.gameObject.SetActive(false);
    }

    public void EmpezarCarrera()
    {
        Player1.GetComponent<Frenado>().RestaurarVel();
        Player1.GetComponent<ControlDireccion>().Habilitado = true;

        if (players != 1)
        {
            Player2.GetComponent<Frenado>().RestaurarVel();
            Player2.GetComponent<ControlDireccion>().Habilitado = true;
        }
    }

    public void FinalizarCarrera()
    {
        GameManager.Instance.EstAct = EstadoJuego.Finalizado;

        GameManager.Instance.TiempoDeJuego = 0;

        if (Player1.Dinero > Player2.Dinero)
        {
            //lado que gano
            if (Player1.LadoActual == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;
            //puntajes
            DatosPartida.PtsGanador = Player1.Dinero;
            DatosPartida.PtsPerdedor = Player2.Dinero;
        }
        else
        {
            //lado que gano
            if (Player2.LadoActual == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

            //puntajes
            DatosPartida.PtsGanador = Player2.Dinero;
            DatosPartida.PtsPerdedor = Player1.Dinero;
        }

        Player1.GetComponent<Frenado>().Frenar();
        Player2.GetComponent<Frenado>().Frenar();

        Player1.ContrDesc.FinDelJuego();
        Player2.ContrDesc.FinDelJuego();
    }

    //cambia a modo de carrera
    public void CambiarACarrera()
    {
        GameManager.Instance.EstAct = EstadoJuego.Jugando;

        for (var i = 0; i < ObjsCarrera.Length; i++) ObjsCarrera[i].SetActive(true);
        
        switch (GameManager.Instance.difficulty)
        {
            case Difficulty.NORMAL:
                obstacles[(int)Difficulty.NORMAL].gameObject.SetActive(true);
                
                normalOffloadStations.SetActive(false);
                normalMoneyBags.SetActive(false);
                break;
            case Difficulty.HARD:
                obstacles[(int)Difficulty.NORMAL].gameObject.SetActive(true);
                obstacles[(int)Difficulty.HARD].gameObject.SetActive(true);
                
                normalOffloadStations.SetActive(false);
                hardOffloadStations.SetActive(false);
                
                normalMoneyBags.SetActive(false);
                hardMoneyBags.SetActive(false);
                break;
            case Difficulty.EASY:
            default:
               break;
        }

        for (var i = 0; i < ObjsCalibracion1.Length; i++) ObjsCalibracion1[i].SetActive(false);
        
        for (var i = 0; i < ObjsCalibracion2.Length; i++) ObjsCalibracion2[i].SetActive(false);

        InitPlayer1();
        if (players != 1)
        {
            InitPlayer2();
            if (Player1.LadoActual == Visualizacion.Lado.Izq)
            {
                Player1.gameObject.transform.position = PosCamionesCarrera[0];
                Player2.gameObject.transform.position = PosCamionesCarrera[1];
            }
            else
            {
                Player1.gameObject.transform.position = PosCamionesCarrera[1];
                Player2.gameObject.transform.position = PosCamionesCarrera[0];
            }
        }
        else
        {
            Player1.gameObject.transform.position = PosCamionesCarrera[2];
            bonusUi.anchoredPosition = new Vector2(950, 0);
        }
        
        tiempoDeJuegoText.transform.parent.gameObject.SetActive(true);
        conteoInicioText.gameObject.SetActive(true);
    }

    public void FinCalibracion(int playerID)
    {
        if (playerID == 0) Player1.FinTuto = true;

        if (playerID == 1) Player2.FinTuto = true;

        if ((Player1.FinTuto && Player2.FinTuto) || (Player1.FinTuto && players == 1))
        {
            CambiarACarrera();
        }
    }
    
    private void SinglePlayerSetup()
    {
        Player2.MiVisualizacion.CamCalibracion.gameObject.SetActive(false);
        Player2.MiVisualizacion.CamConduccion.gameObject.SetActive(false);
        Player2.MiVisualizacion.CamDescarga.gameObject.SetActive(false);
        Player2.MiVisualizacion.uiRoot.SetActive(false);
        Player2.gameObject.SetActive(false);
        
        Player1.MiVisualizacion.CamCalibracion.rect = new Rect(0, 0, 1, 1);
        Player1.MiVisualizacion.CamConduccion.rect = new Rect(0, 0, 1, 1);
        Player1.MiVisualizacion.CamDescarga.rect = new Rect(0, 0, 1, 1);
    }

    private void InitPlayer1()
    {
        Player1.transform.forward = Vector3.forward;
        Player1.GetComponent<Frenado>().Frenar();
        Player1.CambiarAConduccion();
        Player1.GetComponent<Frenado>().RestaurarVel();
        Player1.GetComponent<ControlDireccion>().Habilitado = false;
        Player1.transform.forward = Vector3.forward;
    }

    private void InitPlayer2()
    {
        Player2.transform.forward = Vector3.forward;
        Player2.GetComponent<Frenado>().Frenar();
        Player2.CambiarAConduccion();
        Player2.GetComponent<Frenado>().RestaurarVel();
        Player2.GetComponent<ControlDireccion>().Habilitado = false;
        Player2.transform.forward = Vector3.forward;
    }
}