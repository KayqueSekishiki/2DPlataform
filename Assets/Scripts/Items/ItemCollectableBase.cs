using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compateTag = "Player";
    public ParticleSystem myParticleSystem;
    public Transform parentCoin;


    private void Awake()
    {
        if (myParticleSystem != null)
        {
            myParticleSystem.transform.SetParent(parentCoin);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compateTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        gameObject.SetActive(false);
        OnCollect();
    }

    protected virtual void OnCollect()
    {
        if (myParticleSystem != null)
        {
            myParticleSystem.Play();
        }
    }
}
