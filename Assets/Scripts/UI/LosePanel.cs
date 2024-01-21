using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{

    [SerializeField]
    float tweenTime;
    CanvasGroup losePanelCG;
    [SerializeField]
    CanvasGroup gameControlCG;

    private void Start()
    {
        gameControlCG.interactable = false;
        losePanelCG = GetComponent<CanvasGroup>();
        LeanTween.value(gameObject, (value) => { losePanelCG.alpha = value; },losePanelCG.alpha,1f,tweenTime);
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
