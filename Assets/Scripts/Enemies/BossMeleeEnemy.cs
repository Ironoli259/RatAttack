using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeEnemy : MeleeEnemy
{
    int maxhealth;
    bool hasCalledHelp;    

    public override void OnObjectSpawn()
    {        
        base.OnObjectSpawn();
        hasCalledHelp = false;
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

        if(enemyHP < maxhealth / 2)
        {
            speed = 3;
            if (!hasCalledHelp)
            {
                hasCalledHelp = true;
                animator.SetTrigger("CallHounds");
            }
        }
    }

    public void callHelp()
    {
        for(int i = 0; i < 4; i++)
        {
            objectPooler.SpawnFromPool("RatKnight", transform.position, Quaternion.identity);
        }
    }

    public void OnDeath()
    {
        for (int i = 0; i < 6; i++)
        {
            objectPooler.SpawnFromPool("RatZombie", transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }
}
