
using UnityEngine;
using UnityEditor;

public static class SetupDroneCameras
{
    [MenuItem("Tools/Drone/Setup Cameras")]
    public static void Setup()
    {
        BoatController boat = Object.FindFirstObjectByType<BoatController>();
        if (boat == null)
        {
            Debug.LogError("Aucun BoatController trouvé dans la scène. Place d'abord ton prefab de drone avec BoatController.");
            return;
        }

        GameObject drone = boat.gameObject;

        GameObject followRig = new GameObject("FollowCamera");
        ThirdPersonFollow follow = followRig.AddComponent<ThirdPersonFollow>();
        follow.target = drone.transform;
        follow.offset = new Vector3(0f, 3f, -6f);
        follow.followSpeed = 5f;

        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            GameObject camObj = new GameObject("Main Camera");
            mainCam = camObj.AddComponent<Camera>();
            camObj.tag = "MainCamera";
        }
        mainCam.transform.SetParent(followRig.transform, false);
        mainCam.transform.localPosition = Vector3.zero;
        mainCam.transform.localRotation = Quaternion.identity;

        GameObject fpvHolder = new GameObject("FPVCameraHolder");
        fpvHolder.transform.SetParent(drone.transform);
        fpvHolder.transform.localPosition = new Vector3(0f, 0.8f, 1.0f);

        GameObject fpvCamObj = new GameObject("FPSCamera");
        Camera fpvCam = fpvCamObj.AddComponent<Camera>();
        FPVCamera fpv = fpvCamObj.AddComponent<FPVCamera>();
        fpv.drone = drone.transform;
        fpv.cameraOffset = new Vector3(0f, 0.8f, 1.0f);
        fpvCamObj.transform.SetParent(fpvHolder.transform, false);
        fpvCam.enabled = false;

        GameObject gm = new GameObject("GameManager");
        CameraSwitcher switcher = gm.AddComponent<CameraSwitcher>();
        switcher.thirdPersonCam = mainCam;
        switcher.fpvCam = fpvCam;

        Debug.Log("✅ Caméras 3ème personne + FPV configurées. Touche C pour changer de vue.");
    }
}
