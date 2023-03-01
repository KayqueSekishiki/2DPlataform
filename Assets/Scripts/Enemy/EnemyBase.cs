using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;

    public Animator animator;
    public string triggerAttack = "Attack";
    public string triggerDeath = "Death";

    public HealthBase healthBase;

    public float timeToDestroy = 1f;

    [Header("Spawn Reward")]
    public Transform parentRewards;
    public Transform prefabReward;
    public float timeToReward;


    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnEnemyKill;
        }
    }

    private void OnEnemyKill()
    {
        healthBase.OnKill -= OnEnemyKill;
        GetComponent<CapsuleCollider2D>().enabled = false;
        PlayDeathkAnimation();
        Invoke(nameof(SpawnReward), timeToReward);
        Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();
        if (health != null)
        {
            PlayAttackAnimation();
            health.Damage(damage);
        }
    }

    private void PlayAttackAnimation()
    {
        animator.SetTrigger(triggerAttack);
    }

    private void PlayDeathkAnimation()
    {
        animator.SetTrigger(triggerDeath);
    }

    public void Damage(int amount)
    {
        healthBase.Damage(amount);
    }

    public void SpawnReward()
    {
        prefabReward.transform.position = new Vector2(transform.position.x, transform.position.y + 3f);
        Instantiate(prefabReward, parentRewards);
    }
}
