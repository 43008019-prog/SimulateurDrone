using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Serialization;


[RequireComponent(typeof(Rigidbody))]
public class Buoyancy2 : MonoBehaviour
{
    [Header("Points de flottaison")]
    public Transform[] floatPoints;   // 4 flotteurs

    public GameObject MyWater; 

    [Header("Réglages")]
    public float buoyancyForce = 12f;
    public float waterDrag = 0.5f;
    public float waterAngularDrag = 0.5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        WaterSurface WS = MyWater.GetComponent<WaterSurface>();
    }

    void FixedUpdate()
    {
        if (floatPoints == null || floatPoints.Length == 0)
            return;

        int submerged = 0;

        foreach (Transform p in floatPoints)
        {
            Vector3 wp = p.position;

            // *** Ici est la clé ***
            // On récupère la hauteur réelle de l’eau à ce point
            float waterHeight = GetWaterHeight(wp);

            // Si immergé → appliquer poussée
            if (wp.y < waterHeight)
            {
                float depth = waterHeight - wp.y;

                // Poussée vers le haut
                rb.AddForceAtPosition(Vector3.up * depth * buoyancyForce, wp, ForceMode.Acceleration);
                submerged++;
            }
        }

        // Plus le drone est dans l’eau → plus il est freiné
        if (submerged > 0)
        {
            float factor = (float)submerged / floatPoints.Length;
            rb.linearDamping = Mathf.Lerp(rb.linearDamping, waterDrag, Time.fixedDeltaTime * 2f);
            rb.angularDamping = Mathf.Lerp(rb.angularDamping, waterAngularDrag, Time.fixedDeltaTime * 2f);
        }
    }

    // 🔥 Fonction à modifier selon TON système d'eau
    float GetWaterHeight(Vector3 pos)
    {
        // ⚠️ VERSION BASIQUE (à modifier selon ton eau)
        // Beaucoup d’assets d’eau ont une fonction "GetWaveHeight()" ou "SampleHeight()"

        // Exemple générique :
        return WaterSurface.Instance.GetHeight(pos);
        // SI tu n’as pas cette fonction :
        // return 0f; // hauteur de l’eau fixe
    }
}
