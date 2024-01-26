using UnityEngine;

namespace AmbientCosmetics
{
    public class SkyboxCamera : MonoBehaviour
    {
        
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Transform playerCameraTransform;
        [SerializeField] private Camera thisCamera;
        [SerializeField] private float skyboxScale;
        private void Start()
        {
            playerCamera = Camera.main;
            thisCamera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            transform.rotation = playerCameraTransform.rotation;
            transform.localPosition = playerCameraTransform.position / skyboxScale;
            thisCamera.fieldOfView = playerCamera.fieldOfView;
        }
    }
}
