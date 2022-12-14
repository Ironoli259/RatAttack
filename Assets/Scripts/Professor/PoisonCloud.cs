using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour, IPooledObject
{
    Animator animator;
    public void OnObjectSpawn()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.Damage((int)(1 + (player.PlayerPower / 2)));
        }
    }

    void DestroyObj()
    {
        gameObject.SetActive(false);
    }
}
