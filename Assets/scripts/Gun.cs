using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //[SerializeField] GameObject hiteffect;
    [SerializeField] ParticleSystem muzzeffect;
    [SerializeField] GameObject BulletTrail;
    [SerializeField] float maxdistance;
    [SerializeField] int damage;
    [SerializeField] float Bulletspeed;

    [SerializeField] ParticleSystem bulletEffect;


    public void Fire()
   {
        muzzeffect.Play();
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxdistance))
        {
            IHitable hitable = hit.transform.GetComponent<IHitable>(); //컴포넌트 얻는데다가 인터페이스를 넣어도 찾을수있다.
                                                                       //  GameObject effect = GameManager.Pool.Get(hiteffect, hit.point, Quaternion.LookRotation(hit.normal));
            ParticleSystem effect = GameManager.Resource.Instantiate<ParticleSystem>("prifabs/HitEffect", hit.point, Quaternion.LookRotation(hit.normal), true);
            //  ParticleSystem effect= Instantiate(hiteffect, hit.point, Quaternion.LookRotation(hit.normal));

            // effect.transform.parent = hit.transform;
          //  StartCoroutine(ReleaseRoutin(effect));
        
            // identity= 회전이 없다
            
            StartCoroutine(TrailRoutine(muzzeffect.transform.position,hit.point));
         
            hitable?.Hit(hit, damage);
        }
        else
        {
          
            StartCoroutine(TrailRoutine( muzzeffect.transform.position, Camera.main.transform.forward*maxdistance));
         
        }
   }

    IEnumerator ReleaseRoutin(GameObject effect)
    {
        yield return new WaitForSeconds(3f);
        GameManager.Pool.Release(effect);
    }
    IEnumerator TrailRoutine(Vector3 startpoint, Vector3 endpoint)
    {
        // TrailRenderer trail = Instantiate(BulletTrail, muzzeffect.transform.position, Quaternion.identity);

        GameObject trail = GameManager.Pool.Get(BulletTrail,startpoint,Quaternion.identity);
   
        trail.GetComponent<TrailRenderer>().Clear();

        float totaltime = Vector2.Distance(startpoint, endpoint) / Bulletspeed;

        float rate = 0;
        while (rate < 1)
        {
            trail.transform.position = Vector3.Lerp(startpoint, endpoint, rate);
            rate += Time.deltaTime / totaltime;

            yield return null;
        }


        GameManager.Pool.Release(trail);
        //  Destroy(trail);
        // Destroy(trail.gameObject);

        //풀링시스템은 활성화 비활성화 문제라서 ==null 만 쓰기보다는 activeself 랑 같이사용해서 체크
        // 풀링시스템 이용할때 다시 반납할때 상태가 저장된 형태로 들어갈수 있어서 한번 상태 초기화 해라
        yield return null;

        if (!trail.IsValid())
        {
            Debug.Log("트레일 없다");
        }
        else
        {
            Debug.Log("트레일 있다");
        }
    }
}
