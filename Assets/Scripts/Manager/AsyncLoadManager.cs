using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
internal sealed class AsyncLoadManager : MonoBehaviour
{
    
    private AsyncOperation operation;
    
    [Header("Loading Circle")]
    [SerializeField] Image loadingCircle;
    [SerializeField] float loadingCircleRotateSpeed;
    [SerializeField] CanvasGroup blackPanelCG;

    [Header("Alpha Tween")]
    [SerializeField] float tweenTime;
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        LeanTween.value(gameObject, (value) => { canvasGroup.alpha = value; }, canvasGroup.alpha, 1f, tweenTime);
    }

    private void FixedUpdate()
    {
        loadingCircle.transform.Rotate(new Vector3(0,0,-loadingCircleRotateSpeed*Time.fixedDeltaTime));
    }

    public IEnumerator LoadAsync(int index)
    {
        yield return new WaitForSeconds(3f);

        operation = SceneManager.LoadSceneAsync($"Level{index}" , LoadSceneMode.Single);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress > 0.8)
                break;
            yield return null;
        }

        LeanTween.value(gameObject,(value)=>blackPanelCG.alpha = value,blackPanelCG.alpha,1f,tweenTime)
            .setOnComplete(() => {operation.allowSceneActivation = true;});
    }
}
