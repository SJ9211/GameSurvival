using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTaget;

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer); // 캐스팅시작.원의 반지름.캐스팅방향.길이.대상레이어
        nearestTaget = GetNearest();
    }
    
    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach( RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos,targetPos);

            if ( curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }
}
