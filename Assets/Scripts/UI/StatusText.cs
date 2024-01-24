using TMPro;
using UnityEngine;
using System.Collections;

internal sealed class StatusText : MonoBehaviour
{

    TMP_Text text;
    [SerializeField]
    int fontMin,fontMax;
    [SerializeField]
    float tweenTimeIn,tweenTimeOut,animationTime;
    LTDescr tweenIn, tweenOut;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = "";
    }

    public IEnumerator StartAnimation(string message) 
    {
        if (tweenIn != null)
            LeanTween.cancel(tweenIn.id); 
        if (tweenOut != null)
            LeanTween.cancel(tweenOut.id); 
        text.text = message;
        tweenIn = LeanTween.value(gameObject, (float value) => { text.fontSize = (int)value; },fontMin,fontMax,tweenTimeIn);
        yield return new WaitForSeconds(animationTime);
        tweenOut = LeanTween.value(gameObject, (float value) => { text.fontSize = (int)value; }, fontMax, fontMin, tweenTimeOut).setOnComplete(() => { text.text = ""; });
    }
}
