using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter;

    private void Awake()
    {
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach(GameObject player in characters)
            player.SetActive(false);

        characters[selectedCharacter].SetActive(true);
    }

    public void ChangeNext()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if (selectedCharacter == 1 && !TitleManager.saveData.isProfessorPurchased)
            selectedCharacter++;
        if (selectedCharacter == 2 && !TitleManager.saveData.isPaladinPurchased)
            selectedCharacter++;
        if (selectedCharacter == characters.Length)
            selectedCharacter = 0;

        characters[selectedCharacter].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }

    public void ChangePrevious()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter == -1)
            selectedCharacter = characters.Length - 1;
        if (selectedCharacter == 2 && !TitleManager.saveData.isPaladinPurchased)
            selectedCharacter--;
        if (selectedCharacter == 1 && !TitleManager.saveData.isProfessorPurchased)
            selectedCharacter--;

        characters[selectedCharacter].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }
}
