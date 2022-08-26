using UnityEngine;

public class ReductorVelColl : MonoBehaviour
{
    public float ReduccionVel;
    public string PlayerTag = "Player";
    private bool Usado;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == PlayerTag)
            if (!Usado)
                Chocado();
    }

    public virtual void Chocado()
    {
        Usado = true;
    }
}