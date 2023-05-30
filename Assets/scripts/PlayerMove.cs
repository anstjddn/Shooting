using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float Movespeed;
    [SerializeField] private float Jumpspeed;
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
        controller.Move(movedir*Movespeed* Time.deltaTime);     // ��ŸŸ��- �����ð�(    /s  )
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
        if (GroundCheck()&& yspeed<0)
        {
            yspeed = 0;
        }
        controller.Move(Vector3.up * yspeed * Time.deltaTime);
    }

    private void OnJump(InputValue value)
    {
        if(GroundCheck())
        yspeed = Jumpspeed;
    }
    private bool GroundCheck()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position+Vector3.up*1, 0.5f, Vector3.down, out hit, 0.6f);
    }
}
