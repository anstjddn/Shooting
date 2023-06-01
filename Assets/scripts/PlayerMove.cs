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
    {   // 아래 코드는 월드 기준 움직임
        // controller.Move(movedir*Movespeed* Time.deltaTime);     // 델타타임- 단위시간(    /s  )
        // 캐릭터 컨트롤러는 리지드바디 사용X 외부에서 힘받는걸 없앨려고 따라서 따로 설정해야된다.


        // 캐릭터 순간이동하지말고 부드럽게 이동하게끔 뎀핑시간과 lerp를 준다.
        // 아래는 내가 바라보는 기준으로 움직임
        // magnitude 벡터의 크기
        if (movedir.magnitude <=0) //안움직임
        {
            movespeed = Mathf.Lerp(movespeed, 0, 0.5f);    // Lerp- 서서히감소나 증가할떄 쓴다.
        }
        else if (isWalking)         //움직이는데 걸ㅇ므
        {
            movespeed = Mathf.Lerp(movespeed, Walkspeed, 0.5f);
        }
        else                  // 움직이는데 뜀
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
