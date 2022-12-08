using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskSpawner : BaseWeapon
{
    [SerializeField] GameObject flaskPrefab;
    
    private Vector3 mousePos;    

    void OnEnable()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        Instantiate(flaskPrefab, transform.position, Quaternion.identity);
    }
}
