using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public SOInt keys;
    public GameObject doorOpened;
    public AudioSource audioSource;
    public List<AudioClip> audioClips;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (keys.value <= 0)
        {
            Debug.Log("No Key!");
            audioSource.clip = audioClips[0];
            StartCoroutine(PlaySFX());
            return;
        }
        keys.value--;
        audioSource.clip = audioClips[1];
        audioSource.Play();

        AnimateDoor();
        Invoke(nameof(ActiveDoorTwo), 3f);
        Invoke(nameof(DisableDoorOne), 3f);

    }

    IEnumerator PlaySFX()
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.3f);
        audioSource.Play();
        yield return new WaitForSeconds(0.3f);
        audioSource.Play(); ;
    }

    public void AnimateDoor()
    {
        animator.SetTrigger("Open");
    }

    private void ActiveDoorTwo()
    {
        doorOpened.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    private void DisableDoorOne()
    {
        this.enabled = false;
    }
}
