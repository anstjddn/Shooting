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
        controller.Move(movedir*Movespeed* Time.deltaTime);     // ��ŸŸ��- �����ð�
        // ĳ���� ��Ʈ�ѷ��� ������ٵ� ���X �ܺο��� ���޴°� ���ٷ��� ���� ���� �����ؾߵȴ�.
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
