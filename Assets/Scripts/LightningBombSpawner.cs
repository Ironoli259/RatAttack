using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBombSpawner : BaseWeapon
{
    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
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
                objectPooler.SpawnFromPool("LightningBomb", spawnPosition, Quaternion.identity);
            }
        }
    }

}
