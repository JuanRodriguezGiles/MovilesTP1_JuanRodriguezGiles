using UnityEngine;

public class EstanteLlegada : ManejoPallets
{
    public GameObject Mano;
    public ContrCalibracion ContrCalib;

    public override bool Recibir(Pallet p)
    {
        p.Portador = gameObject;
        base.Recibir(p);
        ContrCalib.FinTutorial();

        return true;
    }
}