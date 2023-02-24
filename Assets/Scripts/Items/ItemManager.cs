using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{
    public TextMeshProUGUI uiTextCoins;
    public int coins;


    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        UpdateCoinsTextUI();
    }


    private void Reset()
    {
        coins = 0;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void UpdateCoinsTextUI()
    {
        if (coins < 0)
        {
            uiTextCoins.text = "X " + coins + " How did you do it?";
        }
        else if (coins >= 0 && coins < 10)
        {
            uiTextCoins.text = "x 0000" + coins;
        }
        else if (coins >= 10 && coins < 100)
        {
            uiTextCoins.text = "x 000" + coins;
        }
        else if (coins >= 100 && coins < 1000)
        {
            uiTextCoins.text = "x 00" + coins;
        }
        else if (coins >= 1000 && coins < 10000)
        {
            uiTextCoins.text = "x 0" + coins;
        }
        else if (coins >= 10000 && coins < 100000)
        {
            uiTextCoins.text = "x 0" + coins;
        }
        else
        {
            uiTextCoins.text = "I'm Rich!";
        }
    }
}
