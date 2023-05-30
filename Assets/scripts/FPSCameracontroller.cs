using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCameracontroller : MonoBehaviour
{
    [SerializeField] Transform cameraRoot;
    [SerializeField] float mouseS;
    private Vector2 lookDelta;              // 카메라는 보통 레이트업테이트사용
    private float xRotation;
    private float yRotation;


    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void LateUpdate()
    {
        Look();
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    private void Look()
    {
        yRotation += lookDelta.x * mouseS * Time.deltaTime;
        xRotation -= lookDelta.y * mouseS * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
}
