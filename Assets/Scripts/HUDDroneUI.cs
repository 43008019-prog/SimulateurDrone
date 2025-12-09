
using UnityEngine;
using UnityEngine.UI;

public class HUDDroneUI : MonoBehaviour
{
    public Rigidbody droneRigidbody;
    public Transform droneTransform;

    public Text speedText;
    public Text headingText;

    void Awake()
    {
        if (droneRigidbody == null || droneTransform == null)
        {
            BoatController boat = Object.FindFirstObjectByType<BoatController>();
            if (boat != null)
            {
                Rigidbody rb = boat.GetComponent<Rigidbody>();
                if (droneRigidbody == null) droneRigidbody = rb;
                if (droneTransform == null) droneTransform = boat.transform;
            }
        }
    }

    void Update()
    {
        if (droneRigidbody != null && speedText != null)
        {
            float speed = droneRigidbody.linearVelocity.magnitude;
            float kmh = speed * 3.6f;
            speedText.text = string.Format("Speed: {0:0.0} m/s ({1:0.0} km/h)", speed, kmh);
        }

        if (droneTransform != null && headingText != null)
        {
            float heading = droneTransform.eulerAngles.y;
            headingText.text = string.Format("Heading: {0:0}°", heading);
        }
    }
}
