using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Action OnKill;

    public int startLife = 10;

    public bool destroyOnKill = false;
    public float delayToKill = 0f;

    public int currentLife;
    private bool _isDead = false;

    public FlashColor flashColor;

    private void Awake()
    {
        Init();
        if (flashColor == null)
        {
            flashColor = GetComponent<FlashColor>();
        }
    }

    private void Init()
    {
        _isDead = false;
        currentLife = startLife;
    }

    public void Damage(int damage)
    {
        if (_isDead) return;

        currentLife -= damage;

        if (currentLife <= 0)
        {
            Kill();
        }

        if (flashColor != null)
        {
            flashColor.Flash();
        }
    }

    private void Kill()
    {
        _isDead = true;

        if (destroyOnKill)
        {
            Destroy(gameObject, delayToKill);
        }

        OnKill?.Invoke();
    }
}
