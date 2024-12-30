using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class CameraSystem : MonoBehaviour
    {
        public static CameraSystem Instance { get; private set; }
        [field: SerializeField] public float CameraZoomSpeed { get; set; } = 10f;
        [field: SerializeField] public Vector2 CameraZoomMinMax { get; set; } = new Vector2(1f, 10f);

        [SerializeField] private Cinemachine.CinemachineVirtualCamera playerCamera;


        //private Cinemachine.Cinemachine3rdPersonFollow thirdPersonFollow;
        private float targetCameraSide = 1f;
        private bool isRightSide = true;
        private float targetCameraDistance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            //thirdPersonFollow = playerCamera.GetCinemachineComponent<Cinemachine.Cinemachine3rdPersonFollow>();
            //targetCameraDistance = thirdPersonFollow.CameraDistance;

            InputSystem.Singleton.OnTab += ToggleCameraSide;
            InputSystem.Singleton.OnScrollWheel += CameraZoomInOut;
        }

        public void Update()
        {
            //thirdPersonFollow.CameraSide = Mathf.Lerp(thirdPersonFollow.CameraSide, targetCameraSide, Time.deltaTime * 5f);
            //thirdPersonFollow.CameraDistance = Mathf.Lerp(thirdPersonFollow.CameraDistance, targetCameraDistance, Time.deltaTime * 5f);
        }

        private void OnDestroy()
        {
            if (InputSystem.Singleton)
            {
                InputSystem.Singleton.OnTab -= ToggleCameraSide;
                InputSystem.Singleton.OnScrollWheel -= CameraZoomInOut;

            }
        }

        public void ChangeCameraSide(bool isRight)
        {
            isRightSide = isRight;
            targetCameraSide = isRight ? 1f : 0f;
        }

        void ToggleCameraSide()
        {
            ChangeCameraSide(!isRightSide);
        }

        void CameraZoomInOut(float delta)
        {
            targetCameraDistance -= delta * CameraZoomSpeed * Time.deltaTime;
            targetCameraDistance = Mathf.Clamp(targetCameraDistance, CameraZoomMinMax.x, CameraZoomMinMax.y);
            // thirdPersonFollow.CameraDistance -= delta * CameraZoomSpeed * Time.deltaTime;
        }
    }
}
