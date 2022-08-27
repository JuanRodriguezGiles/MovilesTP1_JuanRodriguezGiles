using UnityEngine;

public class ManoRecept : ManejoPallets
{
    public bool TengoPallet;

    private void FixedUpdate()
    {
        TengoPallet = Tenencia();
    }

    private void OnTriggerEnter(Collider other)
    {
        var recept = other.GetComponent<ManejoPallets>();
        if (recept != null) Dar(recept);
    }

    //---------------------------------------------------------//	

    public override bool Recibir(Pallet pallet)
    {
        if (!Tenencia())
        {
            pallet.Portador = gameObject;
            base.Recibir(pallet);
            return true;
        }

        return false;
    }

    public override void Dar(ManejoPallets receptor)
    {
        switch (receptor.tag)
        {
            case "Mano":
                if (Tenencia())
                    if (receptor.name == "Right Hand")
                        if (receptor.Recibir(Pallets[0]))
                            Pallets.RemoveAt(0);
                break;

            case "Cinta":
                if (Tenencia())
                    if (receptor.Recibir(Pallets[0]))
                        Pallets.RemoveAt(0);
                break;

            case "Estante":
                break;
        }
    }
}