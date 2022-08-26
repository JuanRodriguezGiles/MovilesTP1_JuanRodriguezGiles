using UnityEngine;

public class Interruptores : MonoBehaviour
{
    public string TagPlayer = "Player";

    public GameObject[] AActivar;

    public bool Activado;

    private void OnTriggerEnter(Collider other)
    {
        if (!Activado)
            if (other.tag == TagPlayer)
            {
                Activado = true;
                for (var i = 0; i < AActivar.Length; i++) AActivar[i].SetActive(true);
            }
    }
}