using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject upgradesMenu;
    [SerializeField] GameObject shopMenu;
    [SerializeField] TMP_Text upGoldValue;
    [SerializeField] TMP_Text goldCost;
    [SerializeField] TMP_Text currentHealth;
    [SerializeField] TMP_Text currentPower;
    [SerializeField] TMP_Text shGoldValue;
    [SerializeField] TMP_Text profText;
    [SerializeField] TMP_Text palaText;

    private int upgradeCost;    

    public static SaveData saveData;
    string SavePath => Path.Combine(Application.persistentDataPath, "save.data");

    private void Awake()
    {
        if (saveData == null)
            Load();
        else
            Save();
    }

    private void Save()
    {
        FileStream file = null;
        try
        {
            if (!Directory.Exists(Application.persistentDataPath))
                Directory.CreateDirectory(Application.persistentDataPath);
            file = File.Create(SavePath);
            var bf = new BinaryFormatter();
            bf.Serialize(file, saveData);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            if (file != null)
                file.Close();
        }
    }

    private void Load()
    {
        FileStream file = null;
        try
        {
            file = File.Open(SavePath, FileMode.Open);
            var bf = new BinaryFormatter();
            saveData = (SaveData)bf.Deserialize(file);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            saveData = new SaveData();
        }
        finally
        {
            if (file != null)
                file.Close();
        }
    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    //----------------- Upgrade Menu -----------------//
    public void OnUpgradeButtonClick()
    {
        upgradesMenu.SetActive(true);
        SetUpgradeStrings();
    }
    public void AddHealth()
    {
        TitleManager.saveData.permHealthBoost++;
        TitleManager.saveData.goldCoins -= upgradeCost;
        SetUpgradeStrings();
    }

    public void AddPower()
    {
        TitleManager.saveData.permPowerBoost++;
        TitleManager.saveData.goldCoins -= upgradeCost;
        SetUpgradeStrings();
    }

    public void SetUpgradeStrings()
    {
        upGoldValue.text = TitleManager.saveData.goldCoins.ToString();
        upgradeCost = (TitleManager.saveData.permHealthBoost + TitleManager.saveData.permPowerBoost) * 10;
        goldCost.text = upgradeCost.ToString();
        currentHealth.text = TitleManager.saveData.permHealthBoost.ToString();
        currentPower.text = TitleManager.saveData.permPowerBoost.ToString();
    }
    //----------------- Shop Menu -----------------//
    public void OnShopButtonClick()
    {
        shopMenu.SetActive(true);
        SetShopStrings();
    }

    public void OnPurchaseProfClick()
    {
        if (TitleManager.saveData.isProfessorPurchased || TitleManager.saveData.goldCoins < 1000)
            return;
        TitleManager.saveData.goldCoins -= 1000;
        TitleManager.saveData.isProfessorPurchased = true;
        SetShopStrings();
    }

    public void OnPurchasePalaClick() 
    {
        if (TitleManager.saveData.isPaladinPurchased || TitleManager.saveData.goldCoins < 1500)
            return;
        TitleManager.saveData.goldCoins -= 1500;
        TitleManager.saveData.isPaladinPurchased = true;
        SetShopStrings();
    }

    public void SetShopStrings()
    {
        shGoldValue.text = TitleManager.saveData.goldCoins.ToString();
        profText.text = (TitleManager.saveData.isProfessorPurchased ? "Purchased" : "Purchase");
        palaText.text = (TitleManager.saveData.isPaladinPurchased ? "Purchased" : "Purchase");
    }

    public void Back()
    {
        upgradesMenu.SetActive(false);
        shopMenu.SetActive(false);
        Save();
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
