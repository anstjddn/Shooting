using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] ParticleSystem hiteffect;
    [SerializeField] ParticleSystem muzzeffect;
    [SerializeField] TrailRenderer BulletTrail;
    [SerializeField] float maxdistance;
    [SerializeField] int damage;
    [SerializeField] float Bulletspeed;

   public void Fire()
   {
        muzzeffect.Play();
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxdistance))
        {
            IHitable hitable = hit.transform.GetComponent<IHitable>(); //������Ʈ ��µ��ٰ� �������̽��� �־ ã�����ִ�.
            ParticleSystem effect= Instantiate(hiteffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.parent = hit.transform;
            Destroy(effect.gameObject, 3f);
            // identity= ȸ���� ����
            TrailRenderer trail = Instantiate(BulletTrail, muzzeffect.transform.position, Quaternion.identity);
            StartCoroutine(TrailRoutine(trail,muzzeffect.transform.position,hit.point));
            Destroy(trail.gameObject, 3f);
            hitable?.Hit(hit, damage);
        }
        else
        {
            TrailRenderer trail = Instantiate(BulletTrail, muzzeffect.transform.position, Quaternion.identity);
            StartCoroutine(TrailRoutine(trail, muzzeffect.transform.position, Camera.main.transform.forward*maxdistance));
            Destroy(trail.gameObject, 3f);
        }
   }
    IEnumerator TrailRoutine(TrailRenderer trail, Vector3 startpoint, Vector3 endpoint)
    {
        float totaltime = Vector2.Distance(startpoint, endpoint) / Bulletspeed;

        float rate = 0;
        while (rate < 1)
        {
            trail.transform.position = Vector3.Lerp(startpoint, endpoint, rate);
            rate += Time.deltaTime / totaltime;

            yield return null;
        }


    }
}
