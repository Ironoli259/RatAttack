using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;
    public static bool isLvlUpActive = false;
    [SerializeField] public GameObject[] levelUpMenus;    

    public static Vector2 startLocation = new Vector2(0, 0);
    public GameObject[] playerPrefabs;
    Player player;
    public static int totalXp;
    public static int goldCoins;
    int characterIndex;

    private void Awake()
    {
        this.characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        Instantiate(playerPrefabs[this.characterIndex], PlayerManager.startLocation, Quaternion.identity);
        Console.WriteLine("Character spawned");
        PlayerManager.isGameOver = false;
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        PlayerManager.totalXp = 0;
        PlayerManager.goldCoins = 0;
    }

    private void Update()
    {
        if (PlayerManager.isLvlUpActive)
        {
            Time.timeScale = 0;
            this.levelUpMenus[this.characterIndex].SetActive(true);        
        }  
        else
            this.levelUpMenus[this.characterIndex].SetActive(false);

        if (PlayerManager.isGameOver)
        {
            Time.timeScale = 0;
            this.gameOverScreen.SetActive(true);
        }
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("Title");
    }

    #region LevelUp Methods
    public void LevelUpAxe()
    {
        PlayerManager.isLvlUpActive = false;
        this.player.weapons[0].LevelUp();
        this.player.weapons[0].transform.localScale = new Vector3(1, 1, 1) * (float)(1 + (0.2 * this.player.weapons[0].level));
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
        Debug.Log("Hammer Leveled up");
    }

    public void LevelUpRedPot()
    {
        PlayerManager.isLvlUpActive = false;
        this.player.weapons[0].level++;
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
        Debug.Log("RedPot Leveled up");
    }

    public void LevelUpHammer()
    {
        PlayerManager.isLvlUpActive = false;
        this.player.weapons[0].level++;
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }

    public void LevelUpLightning()
    {
        PlayerManager.isLvlUpActive = false;
        this.player.weapons[1].LevelUp();
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }

    public void LevelUpGreenPot()
    {
        PlayerManager.isLvlUpActive = false;
        this.player.weapons[1].level++;
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
        Debug.Log("GreenPot Leveled up");
        Debug.Log(this.player.weapons[1].level);
    }

    public void LevelUpHSlash()
    {
        PlayerManager.isLvlUpActive = false;
        this.player.weapons[1].level++;
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }

    public void LevelUpFireball()
    {
        PlayerManager.isLvlUpActive = false;
        this.player.weapons[2].LevelUp();
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }
    public void LevelUpPurplePot()
    {
        PlayerManager.isLvlUpActive = false;
        this.player.weapons[2].level++;
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
        Debug.Log("PurplePot Leveled up");
        Debug.Log(this.player.weapons[2].level);
    }

    public void LevelUpSmite()
    {
        PlayerManager.isLvlUpActive = false;
        this.player.weapons[2].level++;
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }

    public void LevelUpShield()
    {
        PlayerManager.isLvlUpActive = false;
        this.player.weapons[3].LevelUp();
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }
    public void LevelUpArmor()
    {
        PlayerManager.isLvlUpActive = false;
        this.player.weapons[4].LevelUp();
        Armor armor = this.player.weapons[4].GetComponent<Armor>();
        this.player.PlayerMaxHP += armor.armorPoints * this.player.weapons[4].level;
        this.player.PlayerHP = this.player.PlayerMaxHP;
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }
    public void LevelUpPower()
    {
        PlayerManager.isLvlUpActive = false;
        this.player.weapons[5].LevelUp();
        PowerOrb powerOrb = this.player.weapons[5].GetComponent<PowerOrb>();
        this.player.PlayerPower += powerOrb.powerPoints * this.player.weapons[5].level;
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }
    #endregion
}
