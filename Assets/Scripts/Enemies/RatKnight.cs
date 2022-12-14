using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatKnight : MeleeEnemy
{
    public override bool IsDead()
    {
        if (base.enemyHP <= 0)
        {
            GameManager.enemiesKilled[4]++;
            return true;
        }
        return false;
    }
}
