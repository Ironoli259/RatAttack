using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatFarmer : MeleeEnemy
{
    public override bool IsDead()
    {
        if (base.enemyHP <= 0)
        {
            GameManager.enemiesKilled[0]++;
            return true;
        }
        return false;
    }
}
