using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPBar : MonoBehaviour
{
    [SerializeField] GameObject foreground;
    Player player;

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        transform.position = player.transform.position + new Vector3(0, -0.5f, 0);

        // ratio between 0 and 1
        float hpRatio = Math.Abs((float)player.PlayerHP / player.PlayerMaxHP);
        foreground.transform.localScale = new Vector3(hpRatio, 1, 1);
    }
}
