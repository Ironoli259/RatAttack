using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmiteSpawner : BaseWeapon
{    
    ObjectPooler objectPooler;
    Vector3 placement;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void OnEnable()
    {        
        int angle = 30;
        
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0: placement = new Vector3(5,0,0); break;
                case 1: placement = new Vector3(0,5,0); break;
                case 2: placement = new Vector3(-5,0,0); break;
                case 3: placement = new Vector3(0,-5,0); break;
            }
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            objectPooler.SpawnFromPool("Smite", transform.position + placement, rotation);
            angle += 60;
        }
    }
}
