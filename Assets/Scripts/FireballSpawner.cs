using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : BaseWeapon
{
    [SerializeField] GameObject fireballPrefab;
    void Start()
    {
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
                Instantiate(fireballPrefab, transform.position, rotation);
            }
        }
    }
}
