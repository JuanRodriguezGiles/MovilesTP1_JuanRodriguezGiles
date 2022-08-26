using System.Collections.Generic;

using UnityEngine;

public class ManejoPallets : MonoBehaviour
{
    public ControladorDeDescarga Controlador;
    protected int Contador = 0;
    protected List<Pallet> Pallets = new List<Pallet>();

    public virtual bool Recibir(Pallet pallet)
    {
        Pallets.Add(pallet);
        pallet.Pasaje();
        return true;
    }

    public bool Tenencia()
    {
        if (Pallets.Count != 0)
            return true;
        return false;
    }

    public virtual void Dar(ManejoPallets receptor)
    {
        //es el encargado de decidir si le da o no la bolsa
    }
}