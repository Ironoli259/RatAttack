using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBombSpawner : BaseWeapon
{
    [SerializeField] GameObject lightningBombPrefab;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < level; i++)
            {
                Vector3 spawnPosition = UnityEngine.Random.insideUnitCircle * 5;
                spawnPosition += this.transform.position;                
                Instantiate(lightningBombPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

}
