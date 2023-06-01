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
            IHitable hitable = hit.transform.GetComponent<IHitable>(); //컴포넌트 얻는데다가 인터페이스를 넣어도 찾을수있다.
            ParticleSystem effect= Instantiate(hiteffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.parent = hit.transform;
            Destroy(effect.gameObject, 3f);
            // identity= 회전이 없다
            
            StartCoroutine(TrailRoutine(muzzeffect.transform.position,hit.point));
         
            hitable?.Hit(hit, damage);
        }
        else
        {
          
            StartCoroutine(TrailRoutine( muzzeffect.transform.position, Camera.main.transform.forward*maxdistance));
         
        }
   }
    IEnumerator TrailRoutine(Vector3 startpoint, Vector3 endpoint)
    {
        TrailRenderer trail = Instantiate(BulletTrail, muzzeffect.transform.position, Quaternion.identity);
        float totaltime = Vector2.Distance(startpoint, endpoint) / Bulletspeed;

        float rate = 0;
        while (rate < 1)
        {
            trail.transform.position = Vector3.Lerp(startpoint, endpoint, rate);
            rate += Time.deltaTime / totaltime;

            yield return null;
        }

        Destroy(trail);
        Destroy(trail.gameObject);


    }
}
