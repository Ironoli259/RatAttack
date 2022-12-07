using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AI_State { IDLE, CHASING, ATTACK };

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected int enemyHP = 2;
    [SerializeField] protected int enemyMaxHP;
    [SerializeField] protected int damage;
    [SerializeField] protected int attackRange;
    [SerializeField] float attackWaitTimer = 1;

    [SerializeField] Drops[] dropList;

    protected AudioSource source;
    protected GameObject player;
    protected Vector3 direction;
    protected float distanceFromPlayer;

    protected SpriteRenderer spriteRenderer;
    AI_State AIState = AI_State.IDLE;
    protected Animator animator;
    float waitTimer;

    protected virtual void Start()
    {
        this.enemyMaxHP = this.enemyHP;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        waitTimer = attackWaitTimer;
    }

    protected virtual void Update()
    {        
        this.direction = player.transform.position - transform.position;

        switch (AIState)
        {
            case AI_State.IDLE:
                this.waitTimer -= Time.deltaTime;
                if(waitTimer <=0)
                    this.AIState = AI_State.CHASING;
                break;
            case AI_State.CHASING:
                this.distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
                if(distanceFromPlayer > attackRange)
                {
                    animator.SetBool("IsRunning", true);
                    this.transform.localScale = new Vector3(direction.x < 0 ? -1 : 1, 1, 1);
                    Move();
                }
                else
                {
                    animator.SetBool("IsRunning", false);
                    this.AIState = AI_State.ATTACK;
                }
                break;
            case AI_State.ATTACK:
                this.animator.SetTrigger("Attack");
                this.AIState = AI_State.IDLE;
                this.waitTimer = attackWaitTimer;
                break;
            default:
                break;
        }
    }

    protected void Move()
    {
        this.direction.Normalize();
        transform.position += direction * Time.deltaTime * speed;
    }

    public void Damage(int damage)
    {
        this.enemyHP -= damage;
        if (this.enemyHP <= 0)
        {
            source.Play();
            SpawnDrop();
            Destroy(gameObject);

        }
    }

    protected void SpawnDrop()
    {
        Instantiate(dropList[0], this.transform.position, Quaternion.identity);
        int spawnChance = Random.Range(0, 200);

        if (spawnChance < 55)
            Instantiate(dropList[1], this.transform.position, Quaternion.identity);
        else if (spawnChance < 65)
            Instantiate(dropList[2], this.transform.position, Quaternion.identity);
        else if (spawnChance < 69)
            Instantiate(dropList[3], this.transform.position, Quaternion.identity);
        else if (spawnChance < 70)
            Instantiate(dropList[4], this.transform.position, Quaternion.identity);
        else if (spawnChance < 85)
            Instantiate(dropList[5], this.transform.position, Quaternion.identity);
        else if (spawnChance < 92)
            Instantiate(dropList[6], this.transform.position, Quaternion.identity);
        else if (spawnChance < 95)
            Instantiate(dropList[7], this.transform.position, Quaternion.identity);
        else if (spawnChance < 100)
            Instantiate(dropList[8], this.transform.position, Quaternion.identity);
        else if (spawnChance < 105)
            Instantiate(dropList[9], this.transform.position, Quaternion.identity);
    }
}
