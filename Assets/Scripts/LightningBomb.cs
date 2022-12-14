using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBomb : MonoBehaviour, IPooledObject
{ 

    public void OnObjectSpawn()
    {        
        transform.localScale *=  1 + (TitleManager.saveData.permPowerBoost / 5);
        //Destroy Bomb after 4 seconds        
        StartCoroutine(LightningCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        Player player = go.GetComponent<Player>();        
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.Damage(1 + (int)player.PlayerPower);
        }
    }

    IEnumerator LightningCoroutine()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
