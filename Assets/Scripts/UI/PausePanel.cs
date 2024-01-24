using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class PausePanel : MonoBehaviour
{

    [SerializeField]
    float tweenTime;
    CanvasGroup pauseCG;
    [SerializeField]
    CanvasGroup gameControlCG;
    LTDescr pauseTweenInstance;

    private void Awake()
    {
        pauseCG = GetComponent<CanvasGroup>();
    }

    public void OpenPauseMenu()
    {
        gameControlCG.interactable = false;
        if (pauseTweenInstance != null)
            LeanTween.cancel(pauseTweenInstance.id);
        pauseTweenInstance = LeanTween.value(gameObject, (value) => { pauseCG.alpha = value; }, pauseCG.alpha, 1f, tweenTime)
            .setOnComplete(()=>{Time.timeScale = 0f;}) ;
    }

    public void ResumeLevel()
    {
        gameControlCG.interactable = true;
        Time.timeScale = 1f;
        if (pauseTweenInstance != null)
            LeanTween.cancel(pauseTweenInstance.id);
        pauseTweenInstance = LeanTween.value(gameObject, (value) => { pauseCG.alpha = value; }, pauseCG.alpha, 0f, tweenTime)
            .setOnComplete(() => { gameObject.SetActive(false); });
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AllLevels()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection");
    }
}
