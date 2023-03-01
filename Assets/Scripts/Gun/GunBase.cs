using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public ProjectileBase prefabSpecialProjectile;
    public Transform positionShoot;
    public float timeBetweenShoot = .3f;
    public Transform playerSideReference;
    private Coroutine _currentCoroutine;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip specialShootSound;
    public AudioClip noAmmoSound;


    public ItemManager itemManage;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SpecialShoot();
            if (audioSource != null) audioSource.Play();
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            if (audioSource != null) audioSource.Play();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public void Shoot()
    {
        if (audioSource != null) audioSource.clip = shootSound;
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionShoot.position;
        projectile.side = playerSideReference.transform.localScale.x;
    }


    public void SpecialShoot()
    {
        if (itemManage.specialBullets.value == 0)
        {
            Debug.Log("Sem Munições Especiais!");
            if (audioSource != null)
            {
                audioSource.clip = noAmmoSound;
                audioSource.Play();
            }
            return;
        }
        if (audioSource != null) audioSource.clip = specialShootSound;
        itemManage.specialBullets.value--;
        var projectile = Instantiate(prefabSpecialProjectile);
        projectile.transform.position = positionShoot.position;
        projectile.side = playerSideReference.transform.localScale.x;
    }
}
