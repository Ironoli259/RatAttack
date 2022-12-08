using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale *= 1 + (TitleManager.saveData.permPowerBoost / 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.Damage(3 + (int)player.playerPower);
        }
    }

    public void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}