using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IHitable
{

    private Rigidbody Rb;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
    public void Hit(RaycastHit hit, int damage)
    {
        Rb?.AddForceAtPosition(-10 * hit.normal, hit.point, ForceMode.Impulse);
    }
}
