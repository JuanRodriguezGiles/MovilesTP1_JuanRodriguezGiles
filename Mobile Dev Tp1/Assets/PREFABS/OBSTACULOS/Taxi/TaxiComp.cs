using UnityEngine;

/// <summary>
///     basicamente lo que hace es que viaja en linea recta y ocacionalmente gira para un cosatado
///     previamente verificado, tambien cuando llega al final del recorrido se reinicia en la pos. orig.
/// </summary>
public class TaxiComp : MonoBehaviour
{
    public string FinTaxiTag = "FinTaxi";
    public string LimiteTag = "Terreno";

    public float Vel;

    public Vector2 TiempCadaCuantoDobla_MaxMin = Vector2.zero;

    public float DuracionGiro;

    public float AlcanceVerif;

    public string TagTerreno = "";

    public bool Girando;

    public float AngDeGiro = 30;
    private Vector3 PosIni; //para saber donde reiniciar al taxi

    private bool Respawneando;

    private RaycastHit RH;
    private Vector3 RotIni; //pasa saber como volver a su posicion original
    private float TempoDurGir;
    private float TempoEntreGiro;

    private float TiempEntreGiro;

    //-----------------------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
        TiempEntreGiro = Random.Range(TiempCadaCuantoDobla_MaxMin.x, TiempCadaCuantoDobla_MaxMin.y);
        RotIni = transform.localEulerAngles;
        PosIni = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Respawneando)
        {
            if (Medicion())
                Respawn();
        }
        else
        {
            if (Girando)
            {
                TempoDurGir += Time.deltaTime;
                if (TempoDurGir > DuracionGiro)
                {
                    TempoDurGir = 0;
                    DejarDoblar();
                }
            }
            else
            {
                TempoEntreGiro += Time.deltaTime;
                if (TempoEntreGiro > TiempEntreGiro)
                {
                    TempoEntreGiro = 0;
                    Doblar();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * Vel;
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == LimiteTag) Respawneando = true;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == FinTaxiTag)
        {
            transform.position = PosIni;
            transform.localEulerAngles = RotIni;
        }
    }

    //--------------------------------------------------------------------//

    private bool VerificarCostado(Lado lado)
    {
        switch (lado)
        {
            case Lado.Der:
                if (Physics.Raycast(transform.position, transform.right, out RH, AlcanceVerif))
                    if (RH.transform.tag == TagTerreno)
                        return false;
                break;

            case Lado.Izq:
                if (Physics.Raycast(transform.position, transform.right * -1, out RH, AlcanceVerif))
                    if (RH.transform.tag == TagTerreno)
                        return false;
                break;
        }

        return true;
    }

    private void Doblar()
    {
        Girando = true;
        //escoje un lado
        Lado lado;
        if (Random.Range(0, 2) == 0)
        {
            lado = Lado.Izq;
            //verifica, si no da cambia a derecha
            if (!VerificarCostado(lado))
                lado = Lado.Der;
        }
        else
        {
            lado = Lado.Der;
            //verifica, si no da cambia a izq
            if (!VerificarCostado(lado))
                lado = Lado.Izq;
        }


        if (lado == Lado.Der)
        {
            var vaux = transform.localEulerAngles;
            vaux.y += AngDeGiro;
            transform.localEulerAngles = vaux;
        }
        else
        {
            var vaux = transform.localEulerAngles;
            vaux.y -= AngDeGiro;
            transform.localEulerAngles = vaux;
        }
    }

    private void DejarDoblar()
    {
        Girando = false;
        TiempEntreGiro = Random.Range(TiempCadaCuantoDobla_MaxMin.x, TiempCadaCuantoDobla_MaxMin.y);

        transform.localEulerAngles = RotIni;
    }

    private void Respawn()
    {
        Respawneando = false;

        transform.position = PosIni;
        transform.localEulerAngles = RotIni;
    }

    private bool Medicion()
    {
        var dist1 = (GameManager.Instancia.Player1.transform.position - PosIni).magnitude;
        var dist2 = (GameManager.Instancia.Player2.transform.position - PosIni).magnitude;

        if (dist1 > 4 && dist2 > 4)
            return true;
        return false;
    }


    private enum Lado
    {
        Der,
        Izq
    }
}