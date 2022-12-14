using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatRogue : MeleeEnemy
{
    public override bool IsDead()
    {
        if (base.enemyHP <= 0)
        {
            GameManager.enemiesKilled[3]++;
            return true;
        }
        return false;
    }
}
