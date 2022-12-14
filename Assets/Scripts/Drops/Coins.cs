using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Drops
{
    [SerializeField] int amountGold;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            PlayerManager.goldCoins += amountGold;
            gameObject.SetActive(false);
        }
    }
}
