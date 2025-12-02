using UnityEngine;

public class FPVCamera : MonoBehaviour
{
    [Header("Paramètres de la vue FPS")]
    public Transform drone;            // référence du drone
    public Vector3 cameraOffset = new Vector3(0f, 0.8f, 1.0f);
    public float smoothSpeed = 10f;

    [Header("Souris")]
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    void Start()
    {
        // Verrouiller le curseur au centre de l'écran
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (!drone) return;

        // Déplacement souris (pitch uniquement)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -40f, 50f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Position sur le drone
        Vector3 desiredPos = drone.TransformPoint(cameraOffset);
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
    }
}
