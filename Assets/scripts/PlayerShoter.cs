using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoter : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim= GetComponent<Animator>();
    }
    private void OnReload(InputValue Value) 
    {
        anim.SetTrigger("Reload");
    }
    private void OnFire(InputValue Value)
    {
        anim.SetTrigger("Fire");
    }
}
