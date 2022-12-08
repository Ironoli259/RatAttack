using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BaseWeapon
{
    int maxDurability;
    Player player;

    Animator animator;
    int durability;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        maxDurability = 2 * level;
        durability = 1;
        StartCoroutine(ShieldRegenCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Arrow arrow = collision.GetComponent<Arrow>();
        if (arrow)
        {
            durability--;
            
            animator.SetBool("IsDamaged", durability>0);
            
            if (durability <= 0)
            {
                StartCoroutine(ShieldBrokenCoroutine());
            }
            Destroy(arrow);
        }
    }

    IEnumerator ShieldRegenCoroutine()
    {
        while (true)
        {
            maxDurability = 2 * level;
            durability++;
            animator.SetBool("IsDamaged", false);
            if (durability > maxDurability)
                durability = maxDurability;

            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator ShieldBrokenCoroutine()
    {
        player.GenerateShield();
        yield return new WaitForSeconds(2);
        animator.SetBool("IsBroken", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
