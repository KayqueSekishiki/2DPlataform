using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableKey : ItemCollectableBase
{
    public int keyValue;
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddKeys(keyValue);
    }
}
