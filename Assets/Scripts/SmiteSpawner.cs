using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmiteSpawner : BaseWeapon
{
    [SerializeField] GameObject smitePrefab;
    Vector3 placement;
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
            Instantiate(smitePrefab, transform.position + placement, rotation);
            angle += 60;
        }
    }
}
