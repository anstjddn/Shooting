using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float Movespeed;
    private CharacterController controller;
    private Vector3 movedir;
    private float yspeed = 0;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Move();
        Jump();
    }
    private void Move()
    {
        controller.Move(movedir*Movespeed* Time.deltaTime);     // 델타타임- 단위시간
        // 캐릭터 컨트롤러는 리지드바디 사용X 외부에서 힘받는걸 없앨려고 따라서 따로 설정해야된다.
    }
    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        movedir = new Vector3(input.x, 0, input.y);
    }
    private void Jump()
    {
        yspeed += Physics.gravity.y * Time.deltaTime;

        controller.Move(Vector3.up * yspeed * Time.deltaTime);
    }
}
