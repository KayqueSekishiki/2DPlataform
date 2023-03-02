using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase_Ranged : EnemyBase
{
    public float LookRadius;
    public Transform target;

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= LookRadius)
        {
            LookTarget();
            Debug.Log("I See you PLAYER!");
        }
    }

    void LookTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        if (direction.x > 0)
        {
            transform.eulerAngles = new(0, 180);
        }
        else
        {
            transform.eulerAngles = new(0, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }
}
