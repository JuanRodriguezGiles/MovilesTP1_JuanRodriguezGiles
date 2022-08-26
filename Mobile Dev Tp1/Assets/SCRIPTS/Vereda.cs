using UnityEngine;

public class Vereda : MonoBehaviour
{
    public string PlayerTag = "Player";
    public float GiroPorSeg;
    public float RestGiro; // valor que se le suma al giro cuando sale para restaurar la estabilidad

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == PlayerTag) other.SendMessage("SumaGiro", RestGiro);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == PlayerTag) other.SendMessage("SumaGiro", GiroPorSeg * T.GetDT());
    }
}