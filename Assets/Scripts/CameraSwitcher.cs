
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera thirdPersonCam;
    public Camera fpvCam;

    void Start()
    {
        if (thirdPersonCam != null) thirdPersonCam.enabled = true;
        if (fpvCam != null) fpvCam.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (thirdPersonCam == null || fpvCam == null) return;

            bool fpvActive = !fpvCam.enabled;
            fpvCam.enabled = fpvActive;
            thirdPersonCam.enabled = !fpvActive;
        }
    }
}
