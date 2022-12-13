using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IPooledObject
{
    public void OnObjectSpawn()
    {
        Destroy(gameObject, 3);
    }
    
    void Update()
    {
        transform.position += transform.right * 4 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        //Shield shield = collision.GetComponent<Shield>();
        if (player)
        {
            player.OnDamage(1);
            Destroy(gameObject);
        }

    }
}
