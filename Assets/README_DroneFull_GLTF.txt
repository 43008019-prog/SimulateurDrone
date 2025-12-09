
DRONE FULL UNITY PACKAGE (GLTF)
===============================

Contient :
- Assets/Drone/drone_hydrofoil_unity.gltf   ← ton modèle GLTF (renommé)
- Assets/Scripts/BoatController.cs
- Assets/Scripts/ThirdPersonFollow.cs
- Assets/Scripts/FPVCamera.cs
- Assets/Scripts/CameraSwitcher.cs
- Assets/Scripts/SimpleFloat.cs
- Assets/Scripts/HUDDroneUI.cs
- Assets/Editor/DronePrefabBuilder.cs   (Tools -> Drone -> Build Drone Prefab (GLTF))
- Assets/Editor/SetupDroneCameras.cs    (Tools -> Drone -> Setup Cameras)
- Assets/Editor/SetupDroneHUD.cs        (Tools -> Drone -> Setup HUD)

Étapes :

1. Copie le dossier 'Assets' de ce ZIP dans le dossier Assets de ton projet Unity (fusionne si besoin).
2. Ouvre Unity, laisse compiler les scripts.
3. Assure-toi que le plugin glTFast est bien installé (sinon le GLTF ne sera pas vu comme un GameObject).

4. Menu : Tools -> Drone -> Build Drone Prefab (GLTF)
   - Cherche 'drone_hydrofoil_unity' dans Assets/Drone.
   - Instancie le modèle GLTF.
   - Ajoute Rigidbody + MeshCollider (Convex).
   - Crée un enfant 'COM' à (0, -0.3, 0.4) et l'assigne à BoatController.
   - Sauvegarde le prefab : Assets/Drone/DroneHydrofoil.prefab.

5. Sélectionne l'instance de DroneHydrofoil dans la scène :
   - Mets Rotation X = -90, Y = 0 (ou 180 si nécessaire), Z = 0.
   - Mets Scale = (1,1,1).
   - Applique au prefab (Overrides -> Apply All).

6. Pour les caméras :
   Tools -> Drone -> Setup Cameras
   - Crée FollowCamera + Main Camera (3ème personne).
   - Crée FPVCameraHolder + FPSCamera dans le drone.
   - Crée GameManager avec CameraSwitcher (touche C pour switch).

7. Pour le HUD :
   Tools -> Drone -> Setup HUD
   - Crée ou réutilise un Canvas.
   - Ajoute SpeedText & HeadingText en haut à gauche.
   - Ajoute HUDManager avec HUDDroneUI pour afficher vitesse (m/s + km/h) et cap (°).

8. Vérifie dans Project Settings -> Player -> Active Input Handling = Both,
   pour que Input.GetAxis / Input.GetKeyDown fonctionne correctement.

Après ça, tu as un drone GLTF jouable avec :
- conduite physique,
- caméra 3ème personne,
- caméra FPV,
- HUD minimal.
