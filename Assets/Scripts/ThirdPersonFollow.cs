
using UnityEngine;

public class ThirdPersonFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 3f, -6f);
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPos = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);
        transform.LookAt(target);
    }
}
