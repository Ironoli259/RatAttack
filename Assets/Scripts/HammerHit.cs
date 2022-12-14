using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHit : BaseWeapon
{
    Player player;
    CircleCollider2D circleCollider2D;

    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.radius = (float)(0.8 + (level * 0.2));
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.Damage(level + (int)player.PlayerPower + TitleManager.saveData.permPowerBoost);
        }
    }
}
