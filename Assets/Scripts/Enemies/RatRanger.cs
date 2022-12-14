using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatRanger : RangeEnemy
{
    public override bool IsDead()
    {
        if (base.enemyHP <= 0)
        {
            GameManager.enemiesKilled[5]++;
            return true;
        }
        return false;
    }
}
