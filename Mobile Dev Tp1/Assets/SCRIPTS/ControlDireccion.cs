using UnityEngine;

public class ControlDireccion : MonoBehaviour 
{
	public enum TipoInput {AWSD, Arrows}
	public TipoInput InputAct = TipoInput.AWSD;

	private float Giro = 0;
	
	public bool Habilitado = true;
	private CarController carController;

	private int playerID;

	private string inputName = "Horizontal";
	//---------------------------------------------------------//
	
	// Use this for initialization
	private void Start () 
	{
		carController = GetComponent<CarController>();
		playerID = GetComponent<Player>().IdPlayer;
		inputName += playerID;
	}
	
	// Update is called once per frame
	private void Update ()
	{
		Giro = Input.GetAxis(inputName);
		
		// switch(InputAct)
		// {
  //           case TipoInput.AWSD:
  //               if (Habilitado) {
  //                   if (Input.GetKey(KeyCode.A)) {
		// 				Giro = -1;
  //                   }
  //                   else if (Input.GetKey(KeyCode.D)) {
		// 				Giro = 1;
  //                   }
  //                   else {
		// 				Giro = 0;
		// 			}
  //               }
  //               break;
  //           case TipoInput.Arrows:
  //               if (Habilitado) {
  //                   if (Input.GetKey(KeyCode.LeftArrow)) {
		// 				Giro = -1;
		// 			}
  //                   else if (Input.GetKey(KeyCode.RightArrow)) {
		// 				Giro = 1;
		// 			}
  //                   else {
		// 				Giro = 0;
		// 			}
  //               }
  //               break;
  //       }

		carController.SetGiro(Giro);
	}

	public float GetGiro()
	{
		return Giro;
	}
	
}
