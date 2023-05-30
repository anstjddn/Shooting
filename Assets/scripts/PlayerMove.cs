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
    public Animator anim;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        Move();
        Jump();
        
    }
    private void Move()
    {   // �Ʒ� �ڵ�� ���� ���� ������
        // controller.Move(movedir*Movespeed* Time.deltaTime);     // ��ŸŸ��- �����ð�(    /s  )
        // ĳ���� ��Ʈ�ѷ��� ������ٵ� ���X �ܺο��� ���޴°� ���ٷ��� ���� ���� �����ؾߵȴ�.
        // �Ʒ��� ���� �ٶ󺸴� �������� ������
        controller.Move(transform.forward *movedir.z* Movespeed * Time.deltaTime);
        controller.Move(transform.right * movedir.x * Movespeed * Time.deltaTime);
  
    }
    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        movedir = new Vector3(input.x, 0, input.y);
        if( Mathf.Abs(movedir.x) > 0 || Mathf.Abs(movedir.z) > 0)
        {
            anim.SetBool("movemove", true);
        }
        else anim.SetBool("movemove", false);




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
