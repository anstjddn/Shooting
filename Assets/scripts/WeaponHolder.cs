using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] Gun gun;

    public void Fire()
    {
        gun.Fire();
    }
    
}
