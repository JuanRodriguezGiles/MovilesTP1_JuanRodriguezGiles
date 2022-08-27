using System.Collections.Generic;

using UnityEngine;

public class PilaPalletMng : MonoBehaviour
{
    public List<GameObject> BolasasEnCamion = new List<GameObject>();
    public int CantAct;

    // Use this for initialization
    private void Start()
    {
        for (var i = 0; i < BolasasEnCamion.Count; i++) BolasasEnCamion[i].GetComponent<Renderer>().enabled = false;
    }

    public void Sacar()
    {
        BolasasEnCamion[CantAct - 1].GetComponent<Renderer>().enabled = false;
        CantAct--;
    }

    public void Agregar()
    {
        CantAct++;
        BolasasEnCamion[CantAct - 1].GetComponent<Renderer>().enabled = true;
    }
}