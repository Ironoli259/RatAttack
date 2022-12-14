using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AI_State { IDLE, CHASING, ATTACK };

public class Enemy : MonoBehaviour, IPooledObject
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected int enemyHP = 2;
    [SerializeField] protected int enemyMaxHP;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackRange;
    [SerializeField] float attackWaitTimer = 1;

    protected ObjectPooler objectPooler;
    protected AudioSource source;
    protected GameObject player;
    protected Vector3 direction;
    protected float distanceFromPlayer;

    protected SpriteRenderer spriteRenderer;
    AI_State AIState = AI_State.IDLE;
    protected Animator animator;
    float waitTimer;

    protected void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    public virtual void OnObjectSpawn()
    {
        objectPooler = ObjectPooler.Instance;
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
        if (IsDead())
        {
            source.Play();
            SpawnDrop();
            gameObject.SetActive(false);

        }
    }

    public virtual bool IsDead()
    {
        return this.enemyHP <= 0;
    }

    protected void SpawnDrop()
    {
        objectPooler.SpawnFromPool("Crystal", this.transform.position, Quaternion.identity);
        int spawnChance = Random.Range(0, 200);

        if (spawnChance < 55)
            objectPooler.SpawnFromPool("Coin", this.transform.position, Quaternion.identity);
        else if (spawnChance < 65)
            objectPooler.SpawnFromPool("Coins", this.transform.position, Quaternion.identity);
        else if (spawnChance < 69)
            objectPooler.SpawnFromPool("Coins1", this.transform.position, Quaternion.identity);
        else if (spawnChance < 70)
            objectPooler.SpawnFromPool("TreasurePile", this.transform.position, Quaternion.identity);
        else if (spawnChance < 85)
            objectPooler.SpawnFromPool("Cookie", this.transform.position, Quaternion.identity);
        else if (spawnChance < 92)
            objectPooler.SpawnFromPool("ChickenLeg", this.transform.position, Quaternion.identity);
        else if (spawnChance < 95)
            objectPooler.SpawnFromPool("Chicken", this.transform.position, Quaternion.identity);
        else if (spawnChance < 100)
            objectPooler.SpawnFromPool("PowerUp", this.transform.position, Quaternion.identity);
        else if (spawnChance < 105)
            objectPooler.SpawnFromPool("Magnet", this.transform.position, Quaternion.identity);
    }
}
