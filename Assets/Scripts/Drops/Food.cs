using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Drops
{
    [SerializeField] float healthPercentage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.AddHealth(healthPercentage);
            Destroy(gameObject);
        }
    }
}
