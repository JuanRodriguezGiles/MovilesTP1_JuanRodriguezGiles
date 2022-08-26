﻿using UnityEngine;

public class PalletMover : ManejoPallets
{
    public enum MoveType
    {
        WASD,
        Arrows
    }

    public MoveType miInput;

    public ManejoPallets Desde, Hasta;
    private bool segundoCompleto;

    private void Update()
    {
        switch (miInput)
        {
            case MoveType.WASD:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.A)) PrimerPaso();
                if (Tenencia() && Input.GetKeyDown(KeyCode.S)) SegundoPaso();
                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.D)) TercerPaso();
                break;
            case MoveType.Arrows:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.LeftArrow)) PrimerPaso();
                if (Tenencia() && Input.GetKeyDown(KeyCode.DownArrow)) SegundoPaso();
                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.RightArrow)) TercerPaso();
                break;
        }
    }

    private void PrimerPaso()
    {
        Desde.Dar(this);
        segundoCompleto = false;
    }

    private void SegundoPaso()
    {
        Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }

    private void TercerPaso()
    {
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor)
    {
        if (Tenencia())
            if (receptor.Recibir(Pallets[0]))
                Pallets.RemoveAt(0);
    }

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
}