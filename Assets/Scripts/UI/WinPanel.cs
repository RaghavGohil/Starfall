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

    private void Start()
    {
        gameControlCG.interactable = false;
        winPanelCG = GetComponent<CanvasGroup>();
        LevelManager.UnlockLevel(levelToLoad - 1);
        LeanTween.value(gameObject, (value) => { winPanelCG.alpha = value; }, winPanelCG.alpha, 1f, tweenTime);
    }

    public void NextLevel() 
    {
        SceneManager.LoadScene($"Level{levelToLoad}");
    }
    public void ReplayLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
