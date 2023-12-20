using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Cinemachine;

internal sealed class SpeedBlock : MonoBehaviour
{

    [Header("Speed Block Configuration")]
    [SerializeField]
    float speedIncrease;
    [SerializeField]
    float speedTime;
    [SerializeField]
    float speedOrthoSize;
    [SerializeField]
    float speedOrthoTime;
    [SerializeField]
    TrailRenderer lightTrail;
    [SerializeField]
    TrailRenderer darkTrail;
    [SerializeField]
    ParticleSystem smoke;
    [SerializeField]
    Gradient speedLight;
    [SerializeField]
    Gradient speedDark;
    [SerializeField]
    Gradient speedSmoke;
    [SerializeField]
    float speedColorTweenTime;
    CinemachineVirtualCamera vCam;
    bool speedExec = false;

    void Awake()
    {
        vCam = GameObject.FindGameObjectWithTag("vCam").GetComponent<CinemachineVirtualCamera>(); // set vCam tag in unity
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((PlayerMovement.instance.is_dashing || speedExec == true) && collision.transform.tag == "speedBlock")
        {
            if (!speedExec)
                StartCoroutine(speedBlock());
            collision.transform.GetComponent<DestroyBlock>().DestroyIt();
        }
        else if(collision.transform.tag == "speedBlock" && !PlayerMovement.instance.is_dashing)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator speedBlock()
    {
        speedExec = true;
        PlayerMovement.instance.move_speed += speedIncrease;
        PlayerMovement.instance.dash_speed += speedIncrease;
        Gradient lightGradient = lightTrail.colorGradient;
        Gradient darkGradient = darkTrail.colorGradient;
        Gradient smokeGradient = smoke.colorOverLifetime.color.gradient;
        var smokeColor = smoke.colorOverLifetime;
        smokeColor.enabled = true;
        Gradient sGradient = smoke.colorOverLifetime.color.gradient;
        float orthoSize = vCam.m_Lens.OrthographicSize;
        
        LeanTween.value(gameObject, (Color value) => { sGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(value, speedSmoke.colorKeys[0].time), sGradient.colorKeys[1] }, sGradient.alphaKeys); smokeColor.color = sGradient; }, smokeGradient.colorKeys[0].color, speedSmoke.colorKeys[0].color,speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { sGradient.SetKeys(new GradientColorKey[] { sGradient.colorKeys[0] , new GradientColorKey(value, speedSmoke.colorKeys[1].time) }, sGradient.alphaKeys); smokeColor.color = sGradient; }, smokeGradient.colorKeys[1].color, speedSmoke.colorKeys[1].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { smoke.colorOverLifetime.color.gradient.colorKeys[1].color = value; }, smokeGradient.colorKeys[1].color, speedSmoke.colorKeys[1].color,speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);

        LeanTween.value(gameObject, (Color value) => { lightTrail.startColor = value; }, lightGradient.colorKeys[0].color, speedLight.colorKeys[0].color,speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { darkTrail.startColor = value; }, darkGradient.colorKeys[0].color, speedDark.colorKeys[0].color,speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { lightTrail.endColor = value; }, lightGradient.colorKeys[1].color, speedLight.colorKeys[1].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { darkTrail.endColor = value; }, darkGradient.colorKeys[1].color, speedDark.colorKeys[1].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);

        LeanTween.value(gameObject, (float value) => { vCam.m_Lens.OrthographicSize = value; }, orthoSize , speedOrthoSize , speedOrthoTime).setEase(LeanTweenType.easeOutCirc);

        yield return new WaitForSeconds(speedTime);

        LeanTween.value(gameObject, (Color value) => { sGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(value, smokeGradient.colorKeys[0].time), sGradient.colorKeys[1] }, sGradient.alphaKeys); smokeColor.color = sGradient; }, speedSmoke.colorKeys[0].color, smokeGradient.colorKeys[0].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { sGradient.SetKeys(new GradientColorKey[] { sGradient.colorKeys[0], new GradientColorKey(value, smokeGradient.colorKeys[1].time) }, sGradient.alphaKeys); smokeColor.color = sGradient; }, speedSmoke.colorKeys[1].color, smokeGradient.colorKeys[1].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);

        LeanTween.value(gameObject, (Color value) => { lightTrail.startColor = value; }, speedLight.colorKeys[0].color, lightGradient.colorKeys[0].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { darkTrail.startColor = value; }, speedDark.colorKeys[0].color, darkGradient.colorKeys[0].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc );
        LeanTween.value(gameObject, (Color value) => { lightTrail.endColor = value; }, speedLight.colorKeys[1].color, lightGradient.colorKeys[1].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { darkTrail.endColor = value; }, speedDark.colorKeys[1].color, darkGradient.colorKeys[1].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);

        LeanTween.value(gameObject, (float value) => { vCam.m_Lens.OrthographicSize = value; }, speedOrthoSize, orthoSize, speedOrthoTime).setEase(LeanTweenType.easeOutCirc);

        PlayerMovement.instance.move_speed -= speedIncrease;
        PlayerMovement.instance.dash_speed -= speedIncrease;
        speedExec = false;
    }
}