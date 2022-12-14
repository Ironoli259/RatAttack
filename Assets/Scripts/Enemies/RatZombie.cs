using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatZombie : MeleeEnemy
{
    public override bool IsDead()
    {
        if (base.enemyHP <= 0)
        {
            GameManager.enemiesKilled[1]++;
            return true;
        }
        return false;
    }
}
