using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDestroyHelper : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (player != null) player.DestroyMe();
    }
}
