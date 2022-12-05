using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Drops
{
    //Not Done
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.StartMagnet();
            Destroy(gameObject);
        }
    }
}
