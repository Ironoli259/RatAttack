using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAxeHolder : MeleeEnemy
{
    public override bool IsDead()
    {
        if (base.enemyHP <= 0)
        {
            GameManager.enemiesKilled[2]++;
            return true;
        }
        return false;
    }
}
