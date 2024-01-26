using Game.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{

    [SerializeField]
    float tweenTime;
    CanvasGroup winPanelCG;

    [SerializeField]
    int levelToLoad;
    [SerializeField]
    CanvasGroup gameControlCG;

    [HideInInspector]public GameObject player;

    private void Start()
    {
        gameControlCG.interactable = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerMovement>().enabled = false;
        AudioManager.instance.PlayInGame("win");
        winPanelCG = GetComponent<CanvasGroup>();
        LevelManager.UnlockLevel(levelToLoad - 1);
        LeanTween.value(gameObject, (value) => { winPanelCG.alpha = value; }, winPanelCG.alpha, 1f, tweenTime);
    }

    public void NextLevel() 
    {
        SceneManager.LoadScene($"Level{levelToLoad}");
    }
    public void EndGame() 
    {
        SceneManager.LoadScene("End");
    }
    public void ReplayLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
