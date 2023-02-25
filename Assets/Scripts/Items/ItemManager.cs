using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public SOInt keys;
    public SOInt specialBullets;


    private void Start()
    {
        Reset();
    }


    private void Reset()
    {
        coins.value = 0;
        keys.value = 0;
        specialBullets.value = 0;
    }

    public void AddCoins(int amount)
    {
        coins.value += amount;
    }

    public void AddKeys(int amount)
    {
        keys.value += amount;
    }

    public void AddSpecialBullets(int amount)
    {
        specialBullets.value += amount;
    }  
}
