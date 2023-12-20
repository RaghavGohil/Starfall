using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal sealed class StartScreen : MonoBehaviour
{

    [SerializeField]
    GameObject menuPanel;
    [SerializeField]
    GameObject infoPanel;
    [SerializeField]
    GameObject settingsPanel;

    void Awake()
    {
        menuPanel.SetActive(true);
        infoPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void Story()
    {
        SceneManager.LoadScene("LevelSelection");
    }
    public void FreeStyle()
    {
        //-
    }

    public void Info()
    {
        infoPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void Back()
    {
        Awake();
    }
}
