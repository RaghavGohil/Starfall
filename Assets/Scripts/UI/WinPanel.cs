using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{

    [SerializeField]
    float tweenTime;
    CanvasGroup canvasGroup;

    [SerializeField]
    int levelToLoad;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        LeanTween.value(gameObject, (value) => { canvasGroup.alpha = value; }, canvasGroup.alpha, 1f, tweenTime);
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
