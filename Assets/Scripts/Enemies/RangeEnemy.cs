using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    
    [SerializeField] GameObject projectilePrefab;

    private bool isInRange;

    protected override void Start()
    {
        base.Start();
        isInRange = false;
        
    }

    protected override void Update()
    {
        base.Update();

        //Running animation

        if (distanceFromPlayer <= this.attackRange )
        {
            if (!isInRange)
            {
                isInRange = true;
                StartCoroutine(RangeAttackCoroutine());
            }            
        }
        else
        {            
            Move();
        }
        animator.SetBool("IsRunning", this.distanceFromPlayer > attackRange);

    }

    IEnumerator RangeAttackCoroutine()
    {
        animator.SetBool("IsAttacking", true);

        while (distanceFromPlayer <= this.attackRange)
        {
            yield return new WaitForSeconds(2);
            float angle = Quaternion.FromToRotation(transform.right, direction).eulerAngles.z;
            Quaternion qAngle = Quaternion.Euler(0, 0, angle);
            Instantiate(projectilePrefab, transform.position, qAngle);            
            //yield return new WaitForSeconds(1);
        }
        isInRange = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.OnDamage(1);
        }
    }
}
