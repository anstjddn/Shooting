using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float Walkspeed;
    [SerializeField] private float Runspeed;
    [SerializeField] private float Jumpspeed;
    private CharacterController controller;
    private Vector3 movedir;
    private float movespeed;
    private float yspeed = 0;
    public Animator anim;
    private bool isWalking;

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


        // ĳ���� �����̵��������� �ε巴�� �̵��ϰԲ� ���νð��� lerp�� �ش�.
        // �Ʒ��� ���� �ٶ󺸴� �������� ������
        // magnitude ������ ũ��
        if (movedir.magnitude <=0) //�ȿ�����
        {
            movespeed = Mathf.Lerp(movespeed, 0, 0.5f);    // Lerp- ���������ҳ� �����ҋ� ����.
        }
        else if (isWalking)         //�����̴µ� �ɤ���
        {
            movespeed = Mathf.Lerp(movespeed, Walkspeed, 0.5f);
        }
        else                  // �����̴µ� ��
        {
            movespeed = Mathf.Lerp(movespeed, Runspeed, 0.5f);
        }

        controller.Move(transform.forward *movedir.z* movespeed * Time.deltaTime);
        controller.Move(transform.right * movedir.x * movespeed * Time.deltaTime);
        anim.SetFloat("xspeed", movedir.x, 0.1f, Time.deltaTime);
        anim.SetFloat("yspeed", movedir.z, 0.1f, Time.deltaTime);
        anim.SetFloat("speed", movespeed);
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
    private void OnWalk(InputValue value)
    {
        isWalking = value.isPressed;
    }
}
