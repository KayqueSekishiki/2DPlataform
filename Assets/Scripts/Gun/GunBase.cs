using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public GameObject player;

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


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

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

        ShootDirection();


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
    }

    public void ShootDirection()
    {
        if (player.transform.rotation.y == 0)
        {
            prefabProjectile.direction = Vector2.right;
            prefabSpecialProjectile.direction = Vector2.right;
        }
        else
        {
            prefabProjectile.direction = Vector2.left;
            prefabSpecialProjectile.direction = Vector2.left;
        }
    }


}
