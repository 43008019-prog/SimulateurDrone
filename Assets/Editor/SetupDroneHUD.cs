
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public static class SetupDroneHUD
{
    [MenuItem("Tools/Drone/Setup HUD")]
    public static void Setup()
    {
        BoatController boat = Object.FindFirstObjectByType<BoatController>();
        if (boat == null)
        {
            Debug.LogError("Aucun BoatController trouvé dans la scène. Place d'abord ton prefab de drone avec BoatController.");
            return;
        }

        GameObject drone = boat.gameObject;

        Canvas canvas = Object.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("HUDCanvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        Font defaultFont = Resources.GetBuiltinResource<Font>("Arial.ttf");

        GameObject speedGO = new GameObject("SpeedText");
        speedGO.transform.SetParent(canvas.transform);
        Text speedText = speedGO.AddComponent<Text>();
        speedText.font = defaultFont;
        speedText.fontSize = 18;
        speedText.color = Color.white;
        speedText.alignment = TextAnchor.UpperLeft;
        RectTransform speedRT = speedGO.GetComponent<RectTransform>();
        speedRT.anchorMin = new Vector2(0f, 1f);
        speedRT.anchorMax = new Vector2(0f, 1f);
        speedRT.pivot = new Vector2(0f, 1f);
        speedRT.anchoredPosition = new Vector2(15f, -15f);
        speedRT.sizeDelta = new Vector2(260f, 30f);

        GameObject headingGO = new GameObject("HeadingText");
        headingGO.transform.SetParent(canvas.transform);
        Text headingText = headingGO.AddComponent<Text>();
        headingText.font = defaultFont;
        headingText.fontSize = 18;
        headingText.color = Color.white;
        headingText.alignment = TextAnchor.UpperLeft;
        RectTransform headingRT = headingGO.GetComponent<RectTransform>();
        headingRT.anchorMin = new Vector2(0f, 1f);
        headingRT.anchorMax = new Vector2(0f, 1f);
        headingRT.pivot = new Vector2(0f, 1f);
        headingRT.anchoredPosition = new Vector2(15f, -45f);
        headingRT.sizeDelta = new Vector2(260f, 30f);

        GameObject hudGO = new GameObject("HUDManager");
        HUDDroneUI hud = hudGO.AddComponent<HUDDroneUI>();
        hud.droneRigidbody = drone.GetComponent<Rigidbody>();
        hud.droneTransform = drone.transform;
        hud.speedText = speedText;
        hud.headingText = headingText;

        Debug.Log("✅ HUD Drone créé (Speed + Heading en haut à gauche).");
    }
}
