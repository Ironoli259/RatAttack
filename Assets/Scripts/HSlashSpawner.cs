using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSlashSpawner : BaseWeapon
{
    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void OnEnable()
    {
        int angle = 0;
        for (int i = 0; i < 4; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            objectPooler.SpawnFromPool("HolySlash", transform.position, rotation);
            angle += 60;
        }
    }

}
