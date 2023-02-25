using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyHelper : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void KillPlayer()
    {
        player.DestroyMe();
    }
}
