using UnityEngine;
using UnityEngine.SceneManagement;

public enum EstadoJuego
{
    Calibrando,
    Jugando,
    Finalizado,
    MostrandoPuntos
}

public enum Difficulty
{
    EASY,
    NORMAL,
    HARD
}

public class GameManager : Singleton<GameManager>
{
    public float TiempoDeJuego = 60;
    public EstadoJuego EstAct = EstadoJuego.Calibrando;
    public float conteoParaInicion = 3;
    public float tiempoEspMuestraPts = 3;

    [HideInInspector] public int players = 2;
    [HideInInspector] public Difficulty difficulty = Difficulty.EASY;
    [HideInInspector] public LevelController levelController = null;

    private bool conteoRegresivo = true;

    //--------------------------------------------------------//
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SceneConstants.gameplay)
        {
            levelController = FindObjectOfType<LevelController>();
        }
    }

    private void Update()
    {
        //REINICIAR
        if (Input.GetKey(KeyCode.Alpha0)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        switch (EstAct)
        {
            case EstadoJuego.Calibrando:
                if (Input.GetKeyDown(KeyCode.W)) levelController.Player1.Seleccionado = true;
                if (Input.GetKeyDown(KeyCode.UpArrow)) levelController.Player2.Seleccionado = true;
                break;
            case EstadoJuego.Jugando:
                //SKIP LA CARRERA
                if (Input.GetKey(KeyCode.Alpha9)) TiempoDeJuego = 0;
                if (TiempoDeJuego <= 0) levelController.FinalizarCarrera();

                if (conteoRegresivo)
                {
                    conteoParaInicion -= Time.deltaTime;
                    if (conteoParaInicion < 0)
                    {
                        levelController.EmpezarCarrera();
                        conteoRegresivo = false;
                        levelController.tiempoDeJuegoText.transform.parent.gameObject.SetActive(EstAct == EstadoJuego.Jugando && !conteoRegresivo);
                    }
                }
                else
                {
                    TiempoDeJuego -= Time.deltaTime;
                }

                if (conteoRegresivo)
                {
                    if (conteoParaInicion > 1)
                        levelController.conteoInicioText.text = conteoParaInicion.ToString("0");
                    else
                        levelController.conteoInicioText.text = "GO";
                }

                levelController.conteoInicioText.gameObject.SetActive(conteoRegresivo);
                levelController.tiempoDeJuegoText.text = TiempoDeJuego.ToString("00");
                break;
            case EstadoJuego.Finalizado:
                tiempoEspMuestraPts -= Time.deltaTime;
                if (tiempoEspMuestraPts <= 0)
                {
                    EstAct = EstadoJuego.MostrandoPuntos;
                    SceneManager.LoadScene(SceneConstants.endScreen);
                }

                break;
            case EstadoJuego.MostrandoPuntos:
            default:
                break;
        }
    }

    //----------------------------------------------------------//
    public void StartGame(int players)
    {
        this.players = players;

        SceneManager.LoadScene(SceneConstants.gameplay);
    }

    public string PrepararNumeros(int dinero)
    {
        var strDinero = dinero.ToString();
        var res = "";

        if (dinero < 1) //sin ditero
            res = "";
        else if (strDinero.Length == 6) //cientos de miles
            for (var i = 0; i < strDinero.Length; i++)
            {
                res += strDinero[i];

                if (i == 2) res += ".";
            }
        else if (strDinero.Length == 7) //millones
            for (var i = 0; i < strDinero.Length; i++)
            {
                res += strDinero[i];

                if (i == 0 || i == 3) res += ".";
            }

        return res;
    }

    public void SetDifficulty(int diff)
    {
        difficulty = (Difficulty)diff;
    }
}