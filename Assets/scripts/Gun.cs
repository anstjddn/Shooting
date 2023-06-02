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
            IHitable hitable = hit.transform.GetComponent<IHitable>(); //������Ʈ ��µ��ٰ� �������̽��� �־ ã�����ִ�.
                                                                       //  GameObject effect = GameManager.Pool.Get(hiteffect, hit.point, Quaternion.LookRotation(hit.normal));
            ParticleSystem effect = GameManager.Resource.Instantiate<ParticleSystem>("prifabs/HitEffect", hit.point, Quaternion.LookRotation(hit.normal), true);
            //  ParticleSystem effect= Instantiate(hiteffect, hit.point, Quaternion.LookRotation(hit.normal));

            // effect.transform.parent = hit.transform;
          //  StartCoroutine(ReleaseRoutin(effect));
        
            // identity= ȸ���� ����
            
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

        //Ǯ���ý����� Ȱ��ȭ ��Ȱ��ȭ ������ ==null �� ���⺸�ٴ� activeself �� ���̻���ؼ� üũ
        // Ǯ���ý��� �̿��Ҷ� �ٽ� �ݳ��Ҷ� ���°� ����� ���·� ���� �־ �ѹ� ���� �ʱ�ȭ �ض�
        yield return null;

        if (!trail.IsValid())
        {
            Debug.Log("Ʈ���� ����");
        }
        else
        {
            Debug.Log("Ʈ���� �ִ�");
        }
    }
}
