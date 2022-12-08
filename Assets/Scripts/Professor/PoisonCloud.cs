using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.Damage((int)(1 + (player.playerPower / 2)));
        }
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
