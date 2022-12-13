using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeEnemy : MeleeEnemy
{
    int maxhealth;

    public override void OnObjectSpawn()
    {        
        base.OnObjectSpawn();
        maxhealth = enemyHP;

        StartCoroutine(BossCameraCoroutine());
    }

    IEnumerator BossCameraCoroutine()
    {
        Time.timeScale = 0;
        Camera.main.GetComponent<PlayerCamera>().target = gameObject;
        yield return new WaitForSecondsRealtime(5f);
        Camera.main.GetComponent<PlayerCamera>().target = player;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1;
    }

    protected override void Update()
    {
        attackRange = 2.5f;
        base.Update();

        if(enemyHP< maxhealth / 2)
        {
            speed = 3;
        }
    }
}
