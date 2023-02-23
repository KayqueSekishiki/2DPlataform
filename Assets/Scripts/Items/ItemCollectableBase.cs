using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compateTag = "Player";


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compateTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        OnCollect();
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {

    }
}
