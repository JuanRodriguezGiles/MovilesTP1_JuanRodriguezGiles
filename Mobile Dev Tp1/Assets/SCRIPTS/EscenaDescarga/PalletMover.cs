using System;

using UnityEngine;

public class PalletMover : ManejoPallets
{
    public enum Player
    {
        Player1,
        Player2
    }

    public Player miInput;

    public ManejoPallets Desde, Hasta;
    private bool segundoCompleto;

    private void Update()
    {
        switch (miInput)
        {
            case Player.Player1:
                if (!Tenencia() && Desde.Tenencia() && TriggerPrimerPaso()) PrimerPaso();
                if (Tenencia() && TriggerSegundoPaso()) SegundoPaso();
                if (segundoCompleto && Tenencia() && TriggerTercerPaso()) TercerPaso();
                break;
            case Player.Player2:
                if (!Tenencia() && Desde.Tenencia() && TriggerPrimerPaso()) PrimerPaso();
                if (Tenencia() && TriggerSegundoPaso()) SegundoPaso();
                if (segundoCompleto && Tenencia() && TriggerTercerPaso()) TercerPaso();
                break;
        }
    }

    private bool TriggerPrimerPaso()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        switch (miInput)
        {
            case Player.Player1:
                return Input.GetAxisRaw("Horizontal0") < 0;
            case Player.Player2:
                return Input.GetAxisRaw("Horizontal1") < 0;
        }
#elif UNITY_ANDROID || UNITY_IOS
 switch (miInput)
        {
            case Player.Player1:
                return InputManager.Instance.GetAxis("Horizontal0") < -0.5f;
            case Player.Player2:
                return InputManager.Instance.GetAxis("Horizontal1") < -0.5f;
        }
#endif
        return false;
    }

    private bool TriggerSegundoPaso()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        switch (miInput)
        {
            case Player.Player1:
                return Input.GetAxisRaw("Vertical0") < 0;
            case Player.Player2:
                return Input.GetAxisRaw("Vertical1") < 0;
        }
#elif UNITY_ANDROID || UNITY_IOS
 switch (miInput)
        {
            case Player.Player1:
                return InputManager.Instance.GetAxis("Vertical0") < -0.5f;
            case Player.Player2:
                return InputManager.Instance.GetAxis("Vertical1") < -0.5f;
        }
#endif
        return false;
    }

    private bool TriggerTercerPaso()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        switch (miInput)
        {
            case Player.Player1:
                return Input.GetAxisRaw("Horizontal0") > 0;
            case Player.Player2:
                return Input.GetAxisRaw("Horizontal1") > 0;
        }
#elif UNITY_ANDROID || UNITY_IOS
 switch (miInput)
        {
            case Player.Player1:
                return InputManager.Instance.GetAxis("Horizontal0") > 0.5f;
            case Player.Player2:
                return InputManager.Instance.GetAxis("Horizontal1") > 0.5f;
        }
#endif
        return false;
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