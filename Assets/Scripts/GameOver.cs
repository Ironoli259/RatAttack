using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject timer;
    [SerializeField] GameObject ExpBar;
    [SerializeField] TMP_Text farmerCount;
    [SerializeField] TMP_Text zombieCount;
    [SerializeField] TMP_Text axeHolderCount;
    [SerializeField] TMP_Text rogueCount;
    [SerializeField] TMP_Text knightCount;
    [SerializeField] TMP_Text rangerCount;
    [SerializeField] TMP_Text bossCount;
    [SerializeField] TMP_Text goldCount;
    [SerializeField] TMP_Text xpCount;

    private void OnEnable()
    {
        this.timer.SetActive(false);
        this.ExpBar.SetActive(false);
        this.farmerCount.text = GameManager.enemiesKilled[0].ToString();
        this.zombieCount.text = GameManager.enemiesKilled[1].ToString();
        this.axeHolderCount.text = GameManager.enemiesKilled[2].ToString();
        this.rogueCount.text = GameManager.enemiesKilled[3].ToString();
        this.knightCount.text = GameManager.enemiesKilled[4].ToString();
        this.rangerCount.text = GameManager.enemiesKilled[5].ToString();
        this.bossCount.text = GameManager.enemiesKilled[6].ToString();
        this.goldCount.text = PlayerManager.goldCoins.ToString();
    }
}
