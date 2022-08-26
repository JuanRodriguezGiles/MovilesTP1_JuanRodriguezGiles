using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public List<WheelCollider> throttleWheels = new List<WheelCollider>();
    public List<WheelCollider> steeringWheels = new List<WheelCollider>();
    public float throttleCoefficient = 20000f;
    public float maxTurn = 20f;
    private float giro = 0f;
    private float acel = 1f;

	
	// Update is called once per frame
    private void FixedUpdate () {
        foreach (var wheel in throttleWheels) {
            wheel.motorTorque = throttleCoefficient * T.GetFDT() * acel;
        }
        foreach (var wheel in steeringWheels) {
            wheel.steerAngle = maxTurn * giro;
        }
        giro = 0f;
    }

    public void SetGiro(float giro) {
        this.giro = giro;
    }
    public void SetAcel(float val) {
        acel = val;
    }
}
