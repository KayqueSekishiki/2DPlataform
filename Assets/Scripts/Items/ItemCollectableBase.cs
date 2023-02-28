using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compateTag = "Player";
    public ParticleSystem myParticleSystem;
    public Transform parentVFXCoins;
    public Transform parentSFXCoins;

    [Header("Sounds")]
    public AudioSource audioSource;


    private void Awake()
    {
        if (myParticleSystem != null)
        {
            myParticleSystem.transform.SetParent(parentVFXCoins);
        }
        if (audioSource != null)
        {
            audioSource.transform.SetParent(parentSFXCoins);
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
        if (myParticleSystem != null) myParticleSystem.Play();
        if (audioSource != null) audioSource.Play();
    }
}
