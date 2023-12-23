using System;
using UnityEngine;
using UnityEngine.UI;

internal sealed class CursorAnimation : MonoBehaviour
{
    Image image;
    float tweenTime;
    void Start()
    {
        tweenTime = 1f;
        image = GetComponent<Image>();
        LeanTween.value(gameObject, (value) => { image.color = value; }, Color.white, Color.clear, tweenTime).setLoopPingPong();
    }
}
