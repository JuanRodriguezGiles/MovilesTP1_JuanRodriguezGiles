using UnityEngine;

public class CarCamera : MonoBehaviour
{
    public Transform target;
    public float height = 1f;
    public float positionDamping = 3f;
    public float velocityDamping = 3f;
    public float distance = 4f;
    public LayerMask ignoreLayers = -1;

    public float LejaniaZ = 1;

    private Vector3 currentVelocity = Vector3.zero;

    private RaycastHit hit;

    private Vector3 prevVelocity = Vector3.zero;
    private LayerMask raycastLayers = -1;

    private void Start()
    {
        raycastLayers = ~ignoreLayers;
    }

    private void FixedUpdate()
    {
        currentVelocity = Vector3.Lerp(prevVelocity, target.GetComponentInParent<Rigidbody>().velocity, velocityDamping * Time.deltaTime);
        currentVelocity.y = 0;
        prevVelocity = currentVelocity;
    }

    private void LateUpdate()
    {
        var speedFactor = Mathf.Clamp01(target.GetComponentInParent<Rigidbody>().velocity.magnitude / 70.0f);
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(55, 72, speedFactor);
        var currentDistance = Mathf.Lerp(7.5f, 6.5f, speedFactor);

        currentVelocity = currentVelocity.normalized;

        var newTargetPosition = target.position + Vector3.up * height;
        var newPosition = newTargetPosition - currentVelocity * currentDistance;
        newPosition.y = newTargetPosition.y;

        var targetDirection = newPosition - newTargetPosition;
        if (Physics.Raycast(newTargetPosition, targetDirection, out hit, currentDistance, raycastLayers))
            newPosition = hit.point;

        newPosition += transform.forward * LejaniaZ; //diferencia en z agregada por mi

        transform.position = newPosition;
        transform.LookAt(newTargetPosition);

        //rotacion agregada por mi
        var vAux = transform.rotation.eulerAngles;
        vAux.x = 20;
        transform.eulerAngles = vAux;
    }
}