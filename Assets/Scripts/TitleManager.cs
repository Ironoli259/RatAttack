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
    [SerializeField] TMP_Text goldValue;
    [SerializeField] TMP_Text goldCost;
    [SerializeField] TMP_Text currentHealth;
    [SerializeField] TMP_Text currentPower;

    private int cost;
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

    public void OnUpgradeButtonClick()
    {
        upgradesMenu.SetActive(true);
        SetStrings();
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void AddHealth()
    {
        TitleManager.saveData.permHealthBoost++;
        TitleManager.saveData.goldCoins -= cost;
        SetStrings();
    }

    public void AddPower()
    {
        TitleManager.saveData.permPowerBoost++;
        TitleManager.saveData.goldCoins -= cost;
        SetStrings();
    }

    public void SetStrings()
    {
        goldValue.text = TitleManager.saveData.goldCoins.ToString();
        cost = (TitleManager.saveData.permHealthBoost + TitleManager.saveData.permPowerBoost) * 10;
        goldCost.text = cost.ToString();
        currentHealth.text = TitleManager.saveData.permHealthBoost.ToString();
        currentPower.text = TitleManager.saveData.permPowerBoost.ToString();
    }

    public void Back()
    {
        upgradesMenu.SetActive(false);
        Save();
    }
}
