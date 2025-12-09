using UnityEngine;
using UnityEditor;
using System.IO;

public static class DronePrefabBuilder
{
    [MenuItem("Tools/Drone/Build Drone Prefab (GLTF)")]
    public static void BuildDronePrefab()
    {
        string[] guids = AssetDatabase.FindAssets("drone_hydrofoil_unity", new[] { "Assets/Drone" });
        if (guids.Length == 0)
        {
            Debug.LogError("Aucun asset nommé 'drone_hydrofoil_unity' trouvé dans Assets/Drone. Vérifie que ton .gltf est bien importé là.");
            return;
        }

        GameObject modelGO = null;
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (go != null)
            {
                modelGO = go;
                break;
            }
        }

        if (modelGO == null)
        {
            Debug.LogError("Impossible de charger 'drone_hydrofoil_unity' comme GameObject. Vérifie l'import de ton modèle GLTF.");
            return;
        }

        GameObject drone = (GameObject)PrefabUtility.InstantiatePrefab(modelGO);
        drone.name = "DroneHydrofoil";

        Rigidbody rb = drone.GetComponent<Rigidbody>();
        if (rb == null) rb = drone.AddComponent<Rigidbody>();
        rb.mass = 200f;
        rb.linearDamping = 1.2f;
        rb.angularDamping = 4f;
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        MeshCollider mc = drone.GetComponent<MeshCollider>();
        if (mc == null) mc = drone.AddComponent<MeshCollider>();
        mc.convex = true;

        GameObject com = new GameObject("COM");
        com.transform.SetParent(drone.transform);
        com.transform.localPosition = new Vector3(0f, -0.3f, 0.4f);

        BoatController boat = drone.GetComponent<BoatController>();
        if (boat == null) boat = drone.AddComponent<BoatController>();
        boat.comMarker = com.transform;

        string prefabDir = "Assets/Drone";
        if (!AssetDatabase.IsValidFolder(prefabDir))
        {
            AssetDatabase.CreateFolder("Assets", "Drone");
        }

        string prefabPath = Path.Combine(prefabDir, "DroneHydrofoil.prefab").Replace("\\", "/");
        PrefabUtility.SaveAsPrefabAssetAndConnect(drone, prefabPath, InteractionMode.UserAction);

        Debug.Log("✅ Prefab DroneHydrofoil créé depuis GLTF : " + prefabPath);
    }
}