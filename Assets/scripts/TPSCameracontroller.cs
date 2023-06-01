using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCameracontroller : MonoBehaviour
{
    [SerializeField] Transform CameraRoot;
    [SerializeField] float CameraS;
    [SerializeField] float lookdistance;
    [SerializeField] Transform aimTarget;
  
    private float xRotation;
    private float yRotation;
    private Vector2 lookDelta;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
    private void Update()
    {
        Rotate();
    }
    private void Rotate()
    {

        Vector3 lookpoint = Camera.main.transform.position + Camera.main.transform.forward * lookdistance;
        aimTarget.position = lookpoint;
        lookpoint.y = transform.position.y;
        transform.LookAt(lookpoint);
    }
    private void LateUpdate()
    {
        Look();
    }
    private void Look()
    {
        yRotation += lookDelta.x * CameraS * Time.deltaTime;
        xRotation -= lookDelta.y * CameraS * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        CameraRoot.rotation = Quaternion.Euler(xRotation, yRotation, 0);

    }
}
