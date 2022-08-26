using UnityEngine;

public class EstantePartida : ManejoPallets
{
    public GameObject ManoReceptora;

    private void OnTriggerEnter(Collider other)
    {
        var recept = other.GetComponent<ManejoPallets>();
        if (recept != null) Dar(recept);
    }

    //------------------------------------------------------------//

    public override void Dar(ManejoPallets receptor)
    {
        if (receptor.Recibir(Pallets[0])) Pallets.RemoveAt(0);
    }

    public override bool Recibir(Pallet pallet)
    {
        pallet.Portador = gameObject;
        return base.Recibir(pallet);
    }
}