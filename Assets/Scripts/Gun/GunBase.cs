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
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionShoot.position;
        projectile.side = playerSideReference.transform.localScale.x;
    }


    public void SpecialShoot()
    {
        if (itemManage.specialBullets.value == 0)
        {
            Debug.Log("Sem Munições Especiais!");
            return;
        }
        itemManage.specialBullets.value--;
        var projectile = Instantiate(prefabSpecialProjectile);
        projectile.transform.position = positionShoot.position;
        projectile.side = playerSideReference.transform.localScale.x;
    }
}
