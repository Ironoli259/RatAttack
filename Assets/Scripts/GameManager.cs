using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    ObjectPooler objectPooler;
    Player player;
    public static int[] enemiesKilled;

    int totalSeconds, seconds, minutes;

    int spawnCounter = 1;

    void Start()
    {
        if (TitleManager.saveData == null)
        {
            TitleManager.saveData = new SaveData();
        }
        seconds = 0;
        this.objectPooler = ObjectPooler.Instance;
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GameManager.enemiesKilled = new int[7];
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    void Update()
    {
        seconds = (int)Time.time;
        timerText.text = seconds.ToString("00");
        Console.WriteLine("Testing");
    }

    IEnumerator SpawnEnemiesCoroutine()
    {

        SpawnEnemies("RatFarmer", 7);
        yield return new WaitForSeconds(15);
        SpawnEnemies("RatFarmer", 5);
        SpawnEnemies("RatZombie", 8);
        yield return new WaitForSeconds(15);
        SpawnEnemies("RatFarmer", 10);
        SpawnEnemies("RatZombie", 15);
        yield return new WaitForSeconds(20);
        SpawnEnemies("RatZombie", 10);
        SpawnEnemies("RatRanger", 1);
        yield return new WaitForSeconds(15);
        SpawnEnemies("RatFarmer", 5);
        SpawnEnemies("RatAxeHolder", 5);
        SpawnEnemies("RatRanger", 1);
        yield return new WaitForSeconds(15);
        SpawnEnemies("RatFarmer", 3);
        SpawnEnemies("RatAxeHolder", 7);
        SpawnEnemies("RatRogue", 3);
        yield return new WaitForSeconds(10);
        SpawnEnemies("RatAxeHolder", 7);
        SpawnEnemies("RatRogue", 5);
        SpawnEnemies("RatRanger", 2);
        yield return new WaitForSeconds(17);
        SpawnEnemies("RatAxeHolder", 4);
        SpawnEnemies("RatRogue", 5);
        SpawnEnemies("RatKnight", 3);
        SpawnEnemies("RatRanger", 1);
        yield return new WaitForSeconds(20);
        SpawnEnemies("RatZombie", 30);
        yield return new WaitForSeconds(15);
        SpawnEnemies("RatKnight", 8);
        SpawnEnemies("RatRanger", 3);
        yield return new WaitForSeconds(20);
        SpawnEnemies("RatRogue", 10);
        SpawnEnemies("RatKnight", 12);
        SpawnEnemies("RatAxeHolder", 3);
        yield return new WaitForSeconds(30);
        SpawnEnemies("RatBoss", 1);
        yield return new WaitForSeconds(25);

        while (true)
        {
            spawnCounter++;

            SpawnEnemies("RatFarmer", spawnCounter + 5);
            SpawnEnemies("RatZombie", spawnCounter + 6);
            SpawnEnemies("RatAxeHolder", spawnCounter + 5);
            SpawnEnemies("RatRogue", spawnCounter + 4);
            SpawnEnemies("RatKnight", spawnCounter + 4);
            SpawnEnemies("RatRanger", spawnCounter/2);
            //SpawnEnemies(enemies[6], Random.Range(0, spawnCounter/2));
            yield return new WaitForSeconds(25);
        }


    }

    private void SpawnEnemies(string enemyTag, int numberofEnemies)
    {
        for (int i = 0; i < numberofEnemies; i++)
        {
            Vector3 spawnPosition = UnityEngine.Random.insideUnitCircle.normalized * 10;
            spawnPosition += player.transform.position;
            objectPooler.SpawnFromPool(enemyTag, spawnPosition, Quaternion.identity);
        }
    }
}
