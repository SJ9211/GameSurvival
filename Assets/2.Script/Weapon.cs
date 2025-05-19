using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
          if (!GameManager.instance.isLive)
            return;
            
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

    }

    public void LevelUp(float damage, int per)
    {
        this.damage = damage;
        this.count += count;

        if(id == 0)
          Batch();

        player.BroadcastMessage("ApplayGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // 기본적인 세팅
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;  // 부모 오브젝트를 플레이어로 지정
        transform.localPosition = Vector3.zero; // 지역위치인 localPosition을 원점으로 변경

        // Property 세팅
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for(int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                    break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150;
                 Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }

        player.BroadcastMessage("ApplayGear", SendMessageOptions.DontRequireReceiver); // 특정 함수 호출을 모든 자식에게 방송하는 함수
    }
    
    void Batch()
    {
        for (int index=0; index < count; index++)
        {
           Transform bullet;
           
          if( index < transform.childCount)
          {
            bullet = transform.GetChild(index);
          }
          else
          {
             bullet = GameManager.instance.pool.Get(prefabId).transform;
             bullet.parent = transform;
             }       
        bullet.localPosition = Vector3.zero;
           bullet.localRotation = quaternion.identity;

           Vector3 rotVec = Vector3.forward * 360 * index / count;
           bullet.Rotate(rotVec);
           bullet.Translate(bullet.up * 1.0f, Space.World); // 이동방향

           bullet.GetComponent<Bullet>().Init(damage,-1,Vector3.zero); // -1 무한관통
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTaget)
             return;

        Vector3 targerPos = player.scanner.nearestTaget.position; // 위치
        Vector3 dir = targerPos - transform.position; 
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform; // 총알 가져오기
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage,count,dir);
    }
}
