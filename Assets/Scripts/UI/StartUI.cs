using Game.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public void LoadMenu() 
    {
        AudioManager.instance.PlayInGame("uiButtonClick");
        Fader.instance.FadeOut(()=>{ SceneManager.LoadScene("Menu");}
        );
    }
}
