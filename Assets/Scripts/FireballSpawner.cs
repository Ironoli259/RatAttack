using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : BaseWeapon
{    
    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);

            for (int i = 0; i < level; i++)
            {
                float randomAngle = Random.Range(0, 360f);
                Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);
                objectPooler.SpawnFromPool("FireBall", transform.position, rotation);
            }
        }
    }
}
