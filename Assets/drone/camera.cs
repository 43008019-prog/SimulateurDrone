using UnityEngine;

public class ThirdPersonFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;          // Le drone

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0f, 3f, -6f);  // Position relative
    public float followSpeed = 5f;    // Vitesse de suivi
    public float rotateSpeed = 5f;    // Vitesse de rotation lissée

    [Header("Collision")]
    public LayerMask collisionMask;
    public float cameraRadius = 0.3f;

    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;  // récupère la caméra principale
    }

    void LateUpdate()
    {
        if (!target) return;

        // --- POSITION SUIVIE ---
        Vector3 desiredPos = target.TransformPoint(offset);

        // Évite que la caméra traverse les objets
        if (Physics.SphereCast(target.position, cameraRadius, (desiredPos - target.position).normalized,
                               out RaycastHit hit, offset.magnitude, collisionMask))
        {
            desiredPos = hit.point + hit.normal * 0.4f; // Décollage léger de la surface
        }

        // Lissage du mouvement
        cam.position = Vector3.Lerp(cam.position, desiredPos, followSpeed * Time.deltaTime);

        // --- ROTATION SUIVIE ---
        Quaternion desiredRot = Quaternion.LookRotation(target.position - cam.position);
        cam.rotation = Quaternion.Slerp(cam.rotation, desiredRot, rotateSpeed * Time.deltaTime);
    }
}
