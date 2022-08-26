using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum EstadoJuego
    {
        Calibrando,
        Jugando,
        Finalizado
    }

    public static GameManager Instancia;
    public float TiempoDeJuego = 60;

    public EstadoJuego EstAct = EstadoJuego.Calibrando;

    public Player Player1;
    public Player Player2;
    public float conteoParaInicion = 3;
    public Text conteoInicioText;
    public Text tiempoDeJuegoText;

    public float tiempoEspMuestraPts = 3;

    //posiciones de los camiones dependientes del lado que les toco en la pantalla
    //la pos 0 es para la izquierda y la 1 para la derecha
    public Vector3[] PosCamionesCarrera = new Vector3[2];

    //posiciones de los camiones para el tutorial
    public Vector3 PosCamion1Tuto = Vector3.zero;
    public Vector3 PosCamion2Tuto = Vector3.zero;

    //listas de GO que activa y desactiva por sub-escena
    //escena de tutorial
    public GameObject[] ObjsCalibracion1;

    public GameObject[] ObjsCalibracion2;

    //la pista de carreras
    public GameObject[] ObjsCarrera;

    private bool conteoRegresivo = true;

    //--------------------------------------------------------//

    private void Awake()
    {
        Instancia = this;
    }

    private IEnumerator Start()
    {
        yield return null;
        IniciarTutorial();
    }

    private void Update()
    {
        var touches = Input.touches;

        foreach (var VARIABLE in touches)
        {
        }


        //REINICIAR
        if (Input.GetKey(KeyCode.Alpha0)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        switch (EstAct)
        {
            case EstadoJuego.Calibrando:

                if (Input.GetKeyDown(KeyCode.W)) Player1.Seleccionado = true;

                if (Input.GetKeyDown(KeyCode.UpArrow)) Player2.Seleccionado = true;

                break;


            case EstadoJuego.Jugando:

                //SKIP LA CARRERA
                if (Input.GetKey(KeyCode.Alpha9)) TiempoDeJuego = 0;

                if (TiempoDeJuego <= 0) FinalizarCarrera();

                if (conteoRegresivo)
                {
                    conteoParaInicion -= T.GetDT();
                    if (conteoParaInicion < 0)
                    {
                        EmpezarCarrera();
                        conteoRegresivo = false;
                    }
                }
                else
                {
                    //baja el tiempo del juego
                    TiempoDeJuego -= T.GetDT();
                }

                if (conteoRegresivo)
                {
                    if (conteoParaInicion > 1)
                        conteoInicioText.text = conteoParaInicion.ToString("0");
                    else
                        conteoInicioText.text = "GO";
                }

                conteoInicioText.gameObject.SetActive(conteoRegresivo);

                tiempoDeJuegoText.text = TiempoDeJuego.ToString("00");

                break;

            case EstadoJuego.Finalizado:

                //muestra el puntaje

                tiempoEspMuestraPts -= Time.deltaTime;
                if (tiempoEspMuestraPts <= 0)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                break;
        }

        tiempoDeJuegoText.transform.parent.gameObject.SetActive(EstAct == EstadoJuego.Jugando && !conteoRegresivo);
    }

    //----------------------------------------------------------//

    private void IniciarTutorial()
    {
        for (var i = 0; i < ObjsCalibracion1.Length; i++)
        {
            ObjsCalibracion1[i].SetActive(true);
            ObjsCalibracion2[i].SetActive(true);
        }

        for (var i = 0; i < ObjsCarrera.Length; i++) ObjsCarrera[i].SetActive(false);

        Player1.CambiarATutorial();
        Player2.CambiarATutorial();

        tiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        conteoInicioText.gameObject.SetActive(false);
    }

    private void EmpezarCarrera()
    {
        Player1.GetComponent<Frenado>().RestaurarVel();
        Player1.GetComponent<ControlDireccion>().Habilitado = true;

        Player2.GetComponent<Frenado>().RestaurarVel();
        Player2.GetComponent<ControlDireccion>().Habilitado = true;
    }

    private void FinalizarCarrera()
    {
        EstAct = EstadoJuego.Finalizado;

        TiempoDeJuego = 0;

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
    private void CambiarACarrera()
    {
        EstAct = EstadoJuego.Jugando;

        for (var i = 0; i < ObjsCarrera.Length; i++) ObjsCarrera[i].SetActive(true);

        //desactivacion de la calibracion
        Player1.FinCalibrado = true;

        for (var i = 0; i < ObjsCalibracion1.Length; i++) ObjsCalibracion1[i].SetActive(false);

        Player2.FinCalibrado = true;

        for (var i = 0; i < ObjsCalibracion2.Length; i++) ObjsCalibracion2[i].SetActive(false);


        //posiciona los camiones dependiendo de que lado de la pantalla esten
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

        Player1.transform.forward = Vector3.forward;
        Player1.GetComponent<Frenado>().Frenar();
        Player1.CambiarAConduccion();

        Player2.transform.forward = Vector3.forward;
        Player2.GetComponent<Frenado>().Frenar();
        Player2.CambiarAConduccion();

        //los deja andando
        Player1.GetComponent<Frenado>().RestaurarVel();
        Player2.GetComponent<Frenado>().RestaurarVel();
        //cancela la direccion
        Player1.GetComponent<ControlDireccion>().Habilitado = false;
        Player2.GetComponent<ControlDireccion>().Habilitado = false;
        //les de direccion
        Player1.transform.forward = Vector3.forward;
        Player2.transform.forward = Vector3.forward;

        tiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        conteoInicioText.gameObject.SetActive(false);
    }

    public void FinCalibracion(int playerID)
    {
        if (playerID == 0) Player1.FinTuto = true;

        if (playerID == 1) Player2.FinTuto = true;

        if (Player1.FinTuto && Player2.FinTuto)
            CambiarACarrera();
    }
}