using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : BaseWeapon
{
    Player player;
    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider2D;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();        
        transform.localScale *= 1 + (float)(0.2*level);
        StartCoroutine(AxeCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.Damage(level+(int)player.PlayerPower + TitleManager.saveData.permPowerBoost);
        }
        
    }

    IEnumerator AxeCoroutine()
    {
        while (true)
        {
            spriteRenderer.enabled = true;
            polygonCollider2D.enabled = true;
            yield return new WaitForSeconds(0.5f * level);
            spriteRenderer.enabled = false;
            polygonCollider2D.enabled = false;
            yield return new WaitForSeconds(2);
        }
    }
}
