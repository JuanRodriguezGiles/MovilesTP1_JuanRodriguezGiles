using UnityEngine;

public class ControlDireccion : MonoBehaviour
{
    public bool Habilitado = true;
    private CarController carController;

    private float Giro;

    private string inputName = "Horizontal";

    private int playerID;
    //---------------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
        carController = GetComponent<CarController>();
        playerID = GetComponent<Player>().IdPlayer;
        inputName += playerID;
    }

    // Update is called once per frame
    private void Update()
    {
        Giro = InputManager.Instance.GetAxis(inputName);
        

        carController.SetGiro(Giro);
    }

    public float GetGiro()
    {
        return Giro;
    }
}