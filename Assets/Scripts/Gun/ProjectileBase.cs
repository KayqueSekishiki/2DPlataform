using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    public Vector2 direction;
    public float speed = 30f;
    public int bulletDamage = 1;
    public float timeToDestroy = 1f;



    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        myRigidbody2D.velocity = direction * speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.transform.GetComponent<EnemyBase>();

        if (enemy != null)
        {
            enemy.Damage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
