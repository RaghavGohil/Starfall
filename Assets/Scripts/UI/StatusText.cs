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

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = "";
    }

    public IEnumerator StartAnimation(string message) 
    {
        text.text = message;
        LeanTween.value(gameObject, (float value) => { text.fontSize = (int)value; },fontMin,fontMax,tweenTimeIn);
        yield return new WaitForSeconds(animationTime);
        LeanTween.value(gameObject, (float value) => { text.fontSize = (int)value; }, fontMax, fontMin, tweenTimeOut).setOnComplete(() => { text.text = ""; });
    }
}
