using UnityEngine;

public class EnableInPlayerState : MonoBehaviour
{
    public Player.Estados[] MisEstados;

    private Player.Estados prevEstado = Player.Estados.Ninguno;

    public void SetPlayerState(Player.Estados state)
    {
        if (prevEstado != state)
        {
            var activo = false;
            foreach (var estados in MisEstados)
                if (estados == state)
                {
                    activo = true;
                    break;
                }

            gameObject.SetActive(activo);
            prevEstado = state;
        }
    }
}