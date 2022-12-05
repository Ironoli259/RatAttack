using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : MeleeEnemy
{
    enum GiantState { Idle, Chasing, Attack, Berserk }

    [SerializeField] GameObject knifePrefab;
    //private Animator animator;
    GiantState giantState = GiantState.Idle;
    float waitTimer = 2f;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (this.enemyHP < this.enemyMaxHP / 2)
        {
            giantState = GiantState.Berserk;
        }

        switch (giantState)
        {
            case GiantState.Idle:
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    giantState = GiantState.Chasing;
                }
                break;
            case GiantState.Chasing:
                float distance = Vector3.Distance(transform.position, player.transform.position);
                base.Update();
                if (distance > 5)
                {
                    animator.SetBool("IsRunning", true);
                    base.Update();
                }
                else
                {
                    animator.SetBool("IsRunning", false);
                    giantState = GiantState.Attack;
                }
                break;
            case GiantState.Attack:
                animator.SetTrigger("Attack");
                giantState = GiantState.Idle;
                waitTimer = 3f;
                break;
            case GiantState.Berserk:
                animator.SetTrigger("Attack");
                break;

        }
    }

    public void SpawnKnife(int number)
    {
        Instantiate(knifePrefab, transform.position, Quaternion.identity);
    }
}
