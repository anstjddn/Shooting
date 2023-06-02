using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using Mono.Cecil;

public class PlayerShoter : MonoBehaviour
{

    [SerializeField] float ReloadTime;
    [SerializeField] Rig aimrig;
    [SerializeField] WeaponHolder weaponHolder;

    private Animator anim;
    private bool isReloading;
    private void Awake()
    {
   
        anim = GetComponent<Animator>();
        // 어디에 있던 Resources폴더 안에 있는 프리팹을 불러온다. Resources폴더를 특수취급
        // 근데 권장x 불러올수 있지만 항상 load되어있기때문에 빌드하면 무조건 포함되어 나온다.
        // 모바일게임할때 추가 다운로드 하는게 에셋번들방식으로
        // 유니티 워크플로- 드래그앤드롭, 리소스폴더 ,에셋번들

    }
    private void OnReload(InputValue Value) 
    {
        if (isReloading)
            return;
        StartCoroutine(ReloadRoutine());
    }
    IEnumerator ReloadRoutine()
    {
        anim.SetTrigger("Reload");
        isReloading = true;
        aimrig.weight = 0f;
        yield return new WaitForSeconds(ReloadTime);
        isReloading = false;
        aimrig.weight = 1f;
    }
    public void Fire()
    {
        weaponHolder.Fire();
        anim.SetTrigger("Fire");
    }
    private void OnFire(InputValue Value)
    {
        if (isReloading)
            return;
        Fire();
    }
}
