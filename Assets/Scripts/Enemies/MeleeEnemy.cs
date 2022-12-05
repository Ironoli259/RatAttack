using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected float attackRange = 1.5f;
    
    protected override void Start()
    {
        base.Start();  
    }
    
    protected override void Update()
    {
        base.Update();

        Move();
        //Animations animation
        animator.SetBool("IsRunning", this.distanceFromPlayer >= attackRange);
        animator.SetBool("IsAttacking", this.distanceFromPlayer < attackRange);

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
