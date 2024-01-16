using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Cinemachine;
using EZCameraShake;

internal sealed class SpeedBlock : MonoBehaviour
{

    [Header("Speed Block Configuration")]
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
    [SerializeField]
    ParticleSystem speedLines;
    CinemachineVirtualCamera vCam;
    bool speedExec = false;

    PlayerMovement playerMovementScript;
    [SerializeField]
    StatusText statusTextScript;

    void Awake()
    {
        vCam = GameObject.FindGameObjectWithTag("vCam").GetComponent<CinemachineVirtualCamera>(); // set vCam tag in unity
    }

    private void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((playerMovementScript.is_dashing || speedExec == true) && collider.transform.tag == "speedBlock")
        {
            if (!speedExec)
                StartCoroutine(speedBlock());
            collider.transform.GetComponent<DestroyBlock>().DestroyIt();
        }
        else if(collider.transform.tag == "speedBlock" && !playerMovementScript.is_dashing)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator speedBlock()
    {
        speedExec = true;
        StartCoroutine(statusTextScript.StartAnimation("EXTRA SPEED!"));
        StartCoroutine(CineMachineCameraShaker.Instance.ShakeOnce(2f,0.2f));
        speedLines.Play();
        playerMovementScript.BoostSpeed();
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

        playerMovementScript.UnBoostSpeed();
        speedExec = false;
    }
}
