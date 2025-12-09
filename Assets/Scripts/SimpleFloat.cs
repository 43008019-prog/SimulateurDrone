
using UnityEngine;

public class SimpleFloat : MonoBehaviour
{
    public float waterHeight = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;

        if (pos.y < waterHeight)
        {
            pos.y = waterHeight;
            transform.position = pos;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        }
    }
}
