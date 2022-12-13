using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{    
    
    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();  
    }
    
    protected override void Update()
    {
        base.Update();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.OnDamage(damage);
        }
    }
}
