using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] GameObject[] enemies;
    Player player;
    
    int totalSeconds, seconds, minutes;

    int spawnCounter = 1;

    void Start()
    {        
        if (TitleManager.saveData == null)
        {
            TitleManager.saveData = new SaveData();
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //StartCoroutine(SpawnEnemiesCoroutine());
    }

    void Update()
    {
        seconds = (int)Time.time;                
        timerText.text = seconds.ToString("00");
        Console.WriteLine("Testing");
    }

    IEnumerator SpawnEnemiesCoroutine()
    {

        SpawnEnemies(enemies[7], 3);
        yield return new WaitForSeconds(15);
        /*SpawnEnemies(enemies[1], 10);
        SpawnEnemies(enemies[6], 1);
        yield return new WaitForSeconds(15);
        SpawnEnemies(enemies[0], 5);
        SpawnEnemies(enemies[1], 8);
        yield return new WaitForSeconds(15);
        SpawnEnemies(enemies[0], 10);
        SpawnEnemies(enemies[1], 15);
        yield return new WaitForSeconds(20);
        SpawnEnemies(enemies[0], 10);
        SpawnEnemies(enemies[5], 1);        
        yield return new WaitForSeconds(15);
        SpawnEnemies(enemies[0], 5);
        SpawnEnemies(enemies[2], 5);
        SpawnEnemies(enemies[5], 1);
        yield return new WaitForSeconds(15);
        SpawnEnemies(enemies[0], 3);
        SpawnEnemies(enemies[2], 7);
        SpawnEnemies(enemies[3], 3);
        yield return new WaitForSeconds(10);
        SpawnEnemies(enemies[2], 7);
        SpawnEnemies(enemies[3], 5);
        SpawnEnemies(enemies[5], 2);
        yield return new WaitForSeconds(17);
        SpawnEnemies(enemies[2], 4);
        SpawnEnemies(enemies[3], 5);
        SpawnEnemies(enemies[4], 3);
        SpawnEnemies(enemies[5], 1);
        yield return new WaitForSeconds(20);
        SpawnEnemies(enemies[1], 30);
        yield return new WaitForSeconds(15);
        SpawnEnemies(enemies[4], 8);
        SpawnEnemies(enemies[5], 3);
        yield return new WaitForSeconds(20);
        SpawnEnemies(enemies[3], 10);
        SpawnEnemies(enemies[4], 12);
        SpawnEnemies(enemies[5], 3);
        yield return new WaitForSeconds(30);
        SpawnEnemies(enemies[6], 1);
        yield return new WaitForSeconds(25);

        while (true)
        {
            spawnCounter++;
            for(int i = 0; i < 4; i++)
            {
                SpawnEnemies(enemies[i], Random.Range(spawnCounter, (spawnCounter+2)*2));
            }
            SpawnEnemies(enemies[5], Random.Range(0, 6));
            //SpawnEnemies(enemies[6], Random.Range(0, spawnCounter/2));
            yield return new WaitForSeconds(20);
        }
        */
    }

    private void SpawnEnemies(GameObject enemyToSpawn, int numberofEnemies)
    {
        for (int i = 0; i < numberofEnemies; i++)
        {
            Vector3 spawnPosition = UnityEngine.Random.insideUnitCircle.normalized * 10;
            spawnPosition += player.transform.position;
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);            
        }
    }
}
