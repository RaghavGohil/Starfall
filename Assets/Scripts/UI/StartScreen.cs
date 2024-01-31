using Game.Sound;
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
    [SerializeField]AudioSource audioSrc;

    void Awake()
    {
        menuPanel.SetActive(true);
        infoPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void PlaySound() 
    {
        AudioManager.instance.PlayInGame("uiButtonClick");
    }

    public void ReplayVideo() 
    {
        Fader.instance.FadeOut(()=> { 
            SceneManager.LoadScene("Start");
        });
    }
    public void Story()
    {
        PlaySound();
        LeanTween.value(gameObject, (value) => { audioSrc.volume = value; },audioSrc.volume,0f,0.2f);
        Fader.instance.FadeOut(()=> { 
            SceneManager.LoadScene("LevelSelection");
        });
    }
    public void Info()
    {
        infoPanel.SetActive(true);
        menuPanel.SetActive(false);
        PlaySound();
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        menuPanel.SetActive(false);
        PlaySound();
    }

    public void Back()
    {
        Awake();
        PlaySound();
    }
}
