// This script handles the level selector UI.

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal sealed class LevelSelector : MonoBehaviour
{

    static int numPlanets;

    [SerializeField] Button[] buttons;

    [SerializeField] GameObject shopShipsMenu;

    [SerializeField] AsyncLoadManager loadManager;


    void Start()
    {
        shopShipsMenu.SetActive(false);
        SetButtons();
    }

    void SetButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = LevelManager.levelsUnlocked[i];
            buttons[i].GetComponentInChildren<TMP_Text>().text = LevelManager.levelsUnlocked[i] ? "play" : "locked";
        }
    }

    

    public void PlayLevel(int i) 
    {
            StartCoroutine(loadManager.LoadAsync(i));
    }

    public void Back()
    {
        Fader.instance.FadeOut(()=>{ SceneManager.LoadScene("Menu"); });
    }

    public void ShopShips() 
    {
        shopShipsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

}
