
using UnityEngine;

public class FPVCamera : MonoBehaviour
{
    public Transform drone;
    public Vector3 cameraOffset = new Vector3(0f, 0.8f, 1.0f);

    void LateUpdate()
    {
        if (!drone) return;

        transform.position = drone.TransformPoint(cameraOffset);
        transform.rotation = drone.rotation;
    }
}
