using UnityEngine;

public class BolsaAnim : MonoBehaviour
{
    public float GiroVel = 1;

    public Vector3 Amlitud = Vector3.zero;
    public float VelMov = 1;

    public bool Giro = true;
    public bool MovVert = true;
    private bool Iniciado;

    private Vector3 PosIni;
    private bool Subiendo = true;

    //para que inicien a destiempo
    private float TiempInicio;
    private Vector3 vAuxGir = Vector3.zero;
    private Vector3 vAuxPos = Vector3.zero;

    // Use this for initialization
    private void Start()
    {
        PosIni = transform.position;

        TiempInicio = Random.Range(0, 2);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Iniciado)
        {
            if (Giro)
            {
                vAuxGir = Vector3.zero;
                vAuxGir.y = Time.deltaTime * GiroVel;
                transform.localEulerAngles += vAuxGir;
            }

            if (MovVert)
            {
                if (Subiendo)
                {
                    transform.localPosition += Amlitud.normalized * Time.deltaTime * VelMov;

                    if ((transform.position - PosIni).magnitude > Amlitud.magnitude / 2)
                    {
                        Subiendo = false;
                        transform.localPosition -= Amlitud.normalized * Time.deltaTime * VelMov;
                    }
                }
                else
                {
                    transform.localPosition -= Amlitud.normalized * Time.deltaTime * VelMov;
                    if ((transform.position - PosIni).magnitude > Amlitud.magnitude / 2)
                    {
                        Subiendo = true;
                        transform.localPosition += Amlitud.normalized * Time.deltaTime * VelMov;
                    }
                }
            }
        }
        else
        {
            TiempInicio -= Time.deltaTime;
            if (TiempInicio <= 0)
                Iniciado = true;
        }
    }
}