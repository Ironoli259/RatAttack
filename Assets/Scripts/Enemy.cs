using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected int enemyHP = 2;
    [SerializeField] protected int enemyMaxHP;
    [SerializeField] protected int damage;

    [SerializeField] Drops[] dropList;

    protected AudioSource source;
    protected GameObject player;
    protected float distanceFromPlayer;
    protected Vector3 direction;

    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    protected virtual void Start()
    {
        this.enemyMaxHP = this.enemyHP;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        Vector3 destination = player.transform.position;
        Vector3 source = gameObject.transform.position;

        this.direction = destination - source;

        this.distanceFromPlayer = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);

        //Change direction         
        transform.localScale = new Vector3(direction.x < 0 ? -1 : 1, 1, 1);
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
