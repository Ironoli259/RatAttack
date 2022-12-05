using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{    
    Animator animator;
    
    void Start()
    {
        transform.localScale *= 1 + (TitleManager.saveData.permPowerBoost / 5);
        animator = GetComponent<Animator>();
        StartCoroutine(FireballCoroutine());
        //Destroy(gameObject, 3);        
    }

    void Update()
    {
        transform.position += transform.right * 3 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();        
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.Damage((int)(1 + player.playerPower));
            Destroy(gameObject);
        }
    }

    //End animation
    IEnumerator FireballCoroutine()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool("FireballEnd", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);        
    }
}