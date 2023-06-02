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
        // ��� �ִ� Resources���� �ȿ� �ִ� �������� �ҷ��´�. Resources������ Ư�����
        // �ٵ� ����x �ҷ��ü� ������ �׻� load�Ǿ��ֱ⶧���� �����ϸ� ������ ���ԵǾ� ���´�.
        // ����ϰ����Ҷ� �߰� �ٿ�ε� �ϴ°� ���¹���������
        // ����Ƽ ��ũ�÷�- �巡�׾ص��, ���ҽ����� ,���¹���

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
