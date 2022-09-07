using System;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     clase encargada de TODA la visualizacion
///     de cada player, todo aquello que corresconda a
///     cada seccion de la pantalla independientemente
/// </summary>
public class Visualizacion : MonoBehaviour
{
    public enum Lado
    {
        Izq,
        Der
    }

    public Lado LadoAct;

    public GameObject uiRoot;

    //las distintas camaras
    public Camera CamCalibracion;
    public Camera CamConduccion;
    public Camera CamDescarga;

    //EL DINERO QUE SE TIENE
    public Text Dinero;

    //EL VOLANTE
    public Transform volante;

    //PARA EL INVENTARIO
    public float Parpadeo = 0.8f;
    public float TempParp;
    public bool PrimIma = true;
    public Sprite[] InvSprites;

    public Image Inventario;

    //BONO DE DESCARGA
    public GameObject BonusRoot;
    public Image BonusFill;
    public Text BonusText;


    //CALIBRACION MAS TUTO BASICO
    public GameObject TutoCalibrando;
    public GameObject TutoDescargando;
    public GameObject TutoFinalizado;

    private ControlDireccion Direccion;
    private EnableInPlayerState[] enableInPlayerStates;
    private Player Pj;

    //------------------------------------------------------------------//

    private void Awake()
    {
        enableInPlayerStates = uiRoot.GetComponentsInChildren<EnableInPlayerState>(true);
    }

    // Use this for initialization
    private void Start()
    {
        Direccion = GetComponent<ControlDireccion>();
        Pj = GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (Pj.EstAct)
        {
            case Player.Estados.EnConduccion:
                //inventario
                SetInv();
                //contador de dinero
                SetDinero();
                //el volante
                SetVolante();
                break;

            case Player.Estados.EnDescarga:
                //inventario
                SetInv();
                //el bonus
                SetBonus();
                //contador de dinero
                SetDinero();
                break;

            case Player.Estados.EnTutorial:
                SetTuto();
                break;
        }
    }

    //--------------------------------------------------------//

    public void CambiarATutorial()
    {
        CamCalibracion.enabled = true;
        CamConduccion.enabled = false;
        CamDescarga.enabled = false;

        Array.ForEach(enableInPlayerStates, e => e.SetPlayerState(Pj.EstAct));
    }

    public void CambiarAConduccion()
    {
        CamCalibracion.enabled = false;
        CamConduccion.enabled = true;
        CamDescarga.enabled = false;

        Array.ForEach(enableInPlayerStates, e => e.SetPlayerState(Pj.EstAct));
    }

    public void CambiarADescarga()
    {
        CamCalibracion.enabled = false;
        CamConduccion.enabled = false;
        CamDescarga.enabled = true;

        Array.ForEach(enableInPlayerStates, e => e.SetPlayerState(Pj.EstAct));
    }

    //---------//

    public void SetLado(Lado lado)
    {
        LadoAct = lado;

        var r = new Rect();
        r.width = CamConduccion.rect.width;
        r.height = CamConduccion.rect.height;
        r.y = CamConduccion.rect.y;

        switch (lado)
        {
            case Lado.Der:
                r.x = 0.5f;
                break;


            case Lado.Izq:
                r.x = 0;
                break;
        }

        CamCalibracion.rect = r;
        CamConduccion.rect = r;
        CamDescarga.rect = r;
    }

    private void SetBonus()
    {
        if (Pj.ContrDesc.PEnMov != null)
        {
            BonusRoot.SetActive(true);

            //el fondo
            var bonus = Pj.ContrDesc.Bonus;
            float max = (int)Pallet.Valores.Valor1;
            var t = bonus / max;
            BonusFill.fillAmount = t;
            //la bolsa
            BonusText.text = "$" + Pj.ContrDesc.Bonus.ToString("0");
        }
        else
        {
            BonusRoot.SetActive(false);
        }
    }

    private void SetDinero()
    {
        Dinero.text = GameManager.Instance.PrepararNumeros(Pj.Dinero);
    }

    private void SetTuto()
    {
        switch (Pj.ContrCalib.EstAct)
        {
            case ContrCalibracion.Estados.Calibrando:
                TutoCalibrando.SetActive(true);
                TutoDescargando.SetActive(false);
                TutoFinalizado.SetActive(false);
                break;

            case ContrCalibracion.Estados.Tutorial:
                TutoCalibrando.SetActive(false);
                TutoDescargando.SetActive(true);
                TutoFinalizado.SetActive(false);
                break;

            case ContrCalibracion.Estados.Finalizado:
                TutoCalibrando.SetActive(false);
                TutoDescargando.SetActive(false);
                TutoFinalizado.SetActive(true);
                break;
        }
    }

    private void SetVolante()
    {
        var angulo = -45 * Direccion.GetGiro();
        var rot = volante.localEulerAngles;
        rot.z = angulo;
        volante.localEulerAngles = rot;
    }

    private void SetInv()
    {
        var contador = 0;
        for (var i = 0; i < 3; i++)
            if (Pj.bolsas[i] != null)
                contador++;

        if (contador >= 3)
        {
            TempParp += Time.deltaTime;

            if (TempParp >= Parpadeo)
            {
                TempParp = 0;
                if (PrimIma)
                    PrimIma = false;
                else
                    PrimIma = true;


                if (PrimIma)
                    Inventario.sprite = InvSprites[3];
                else
                    Inventario.sprite = InvSprites[4];
            }
        }
        else
        {
            Inventario.sprite = InvSprites[contador];
        }
    }
}