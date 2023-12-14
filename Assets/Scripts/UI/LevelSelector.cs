// This script handles the level selector UI.

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Helper;

public class LevelSelector : MonoBehaviour
{

    static int numPlanets;

    static bool[] levelLocks;

    [SerializeField]
    Button[] buttons;

    [SerializeField]
    GameObject shopShipsMenu;


    void Awake()
    {
        shopShipsMenu.SetActive(false);
        SetButtons();
    }

    public static void SetLevelLocks() 
    {
        numPlanets = 5;
        LevelSelector.levelLocks = new bool[numPlanets];
        for(int i=0;i < LevelSelector.levelLocks.Length;i++) 
        {
            LevelSelector.levelLocks[i] = (i==0)?true:false;
        }
    }

    void SetButtons()
    {
        for(int i=0;i<buttons.Length;i++) 
        {
            buttons[i].interactable = LevelSelector.levelLocks[i];
            buttons[i].GetComponentInChildren<TMP_Text>().text = LevelSelector.levelLocks[i]?"play":"locked";
        }
    }

    public void UnlockLevel(int i) 
    {
        LevelSelector.levelLocks[i] = true;
    }

    public void PlayLevel(int i) 
    {
        try
        {
            SceneManager.LoadScene($"Level{i}");
        }
        catch
        {
            Logging.Log($"Error loading level {i}.",2);
        }
    }

    public void Back() 
    {
        SceneManager.LoadScene("Menu");
    }

    public void ShopShips() 
    {
        shopShipsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

}
