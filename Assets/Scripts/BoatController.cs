
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    [Header("Mouvement")]
    public float thrustForce = 800f;
    public float turnTorque = 400f;
    public float maxSpeed = 15f;

    [Header("Stabilité")]
    public Transform comMarker;
    public float extraWaterDrag = 0.5f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.mass = 200f;
        rb.linearDamping = 1.2f;
        rb.angularDamping = 4f;
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        if (comMarker != null)
            rb.centerOfMass = comMarker.localPosition;
    }

    void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float turnInput    = Input.GetAxis("Horizontal");

        Vector3 forward = transform.forward;
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        float speed = horizontalVelocity.magnitude;

        if (speed < maxSpeed)
        {
            rb.AddForce(forward * forwardInput * thrustForce, ForceMode.Force);
        }

        if (Mathf.Abs(turnInput) > 0.01f)
        {
            rb.AddTorque(Vector3.up * turnInput * turnTorque, ForceMode.Force);
        }

        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            Vector3 velLocal = transform.InverseTransformDirection(rb.linearVelocity);
            velLocal.x *= 0.1f;
            rb.linearVelocity = transform.TransformDirection(velLocal);
        }
    }
}
