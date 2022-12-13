using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskSpawner : BaseWeapon
{
    [SerializeField] string flaskTag;
    ObjectPooler objectPooler;
    
    private Vector3 mousePos;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void OnEnable()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        objectPooler.SpawnFromPool(flaskTag, transform.position, Quaternion.identity);
    }
}
