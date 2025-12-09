using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public static class AutoWaterCollider
{
    [MenuItem("Tools/Water/Auto Setup Water Collider")]
    public static void SetupWaterCollider()
    {
        // 1) Essayer de trouver un objet "Water" dans la scène
        GameObject water = GameObject.Find("Water");

        if (water == null)
        {
            // Si aucun objet "Water", demander la sélection
            if (Selection.activeGameObject == null)
            {
                Debug.LogError("❌ Aucun objet nommé 'Water' trouvé. Sélectionne ton objet d'eau dans la Hierarchy ou renomme-le en 'Water'.");
                return;
            }

            water = Selection.activeGameObject;
            Debug.Log("ℹ Aucun objet 'Water' trouvé par nom, j'utilise l'objet sélectionné : " + water.name);
        }
        else
        {
            Debug.Log("✔ Objet d'eau trouvé par nom : 'Water'.");
        }

        // 2) Créer / garantir un Layer 'Water'
        CreateWaterLayer();

        int waterLayer = LayerMask.NameToLayer("Water");
        if (waterLayer == -1)
        {
            Debug.LogError("❌ Impossible de trouver ou créer le layer 'Water'. Vérifie ton TagManager.");
            return;
        }

        // Appliquer le layer à l'objet eau + tous ses enfants
        SetLayerRecursively(water, waterLayer);

        // 3) Ajouter / récupérer un BoxCollider
        BoxCollider col = water.GetComponent<BoxCollider>();
        if (col == null)
        {
            col = water.AddComponent<BoxCollider>();
            Debug.Log("✔ BoxCollider ajouté sur l'objet d'eau.");
        }

        // 4) Configurer le collider comme grande surface horizontale
        col.isTrigger = false;

        // Centré sur l'objet, très large
        col.center = Vector3.zero;
        col.size = new Vector3(1000f, 1f, 1000f); // ajustable si ta scène est plus grande/petite

        Debug.Log("✅ Eau configurée : Layer='Water' + BoxCollider surfacique prêt pour la flottaison.");
    }

    private static void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    private static void CreateWaterLayer()
    {
        SerializedObject tagManager = new SerializedObject(
            AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]
        );

        SerializedProperty layersProp = tagManager.FindProperty("layers");

        // Si "Water" existe déjà → on ne fait rien
        for (int i = 8; i < layersProp.arraySize; i++)
        {
            SerializedProperty sp = layersProp.GetArrayElementAtIndex(i);
            if (sp != null && sp.stringValue == "Water")
                return;
        }

        // Sinon, on cherche un slot vide
        for (int i = 8; i < layersProp.arraySize; i++)
        {
            SerializedProperty sp = layersProp.GetArrayElementAtIndex(i);
            if (sp != null && string.IsNullOrEmpty(sp.stringValue))
            {
                sp.stringValue = "Water";
                tagManager.ApplyModifiedProperties();
                Debug.Log("✔ Layer 'Water' créé automatiquement.");
                return;
            }
        }

        Debug.LogWarning("⚠ Aucun slot de layer libre pour créer 'Water'. Tu devras en libérer un manuellement.");
    }
}
