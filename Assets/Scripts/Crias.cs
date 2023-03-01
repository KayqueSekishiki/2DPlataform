using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crias : MonoBehaviour
{
    public int indexLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log(collision.transform.name);
            SceneManager.LoadScene(indexLevel);
        }
    }
}
