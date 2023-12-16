using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

internal sealed class BlockCollision : MonoBehaviour
{
    PlayerMovement playerMovementScript;

    [Header("Speed Block Configuration")]
    [SerializeField]
    float speedIncrease;
    [SerializeField]
    float speedTime;
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
    Gradient sGradient;
    [SerializeField]
    float speedColorTweenTime;
    bool speedExec = false;


    void Start() => playerMovementScript = gameObject.GetComponent<PlayerMovement>();

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerMovementScript.is_dashing) 
        {
            switch (collision.transform.tag) 
            {
                case "debrisBlock":
                    collision.transform.GetComponent<DestroyBlock>().DestroyIt();
                    break;
                case "speedBlock":
                    if(!speedExec)
                        StartCoroutine(speedBlock());
                    collision.transform.GetComponent<DestroyBlock>().DestroyIt();
                    break;
            }
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator speedBlock()
    {
        speedExec = true;
        playerMovementScript.move_speed += speedIncrease;
        playerMovementScript.dash_speed += speedIncrease;
        Gradient lightGradient = lightTrail.colorGradient;
        Gradient darkGradient = darkTrail.colorGradient;
        Gradient smokeGradient = smoke.colorOverLifetime.color.gradient;
        var smokeColor = smoke.colorOverLifetime;
        smokeColor.enabled = true;
        sGradient = smoke.colorOverLifetime.color.gradient;
        
        LeanTween.value(gameObject, (Color value) => { sGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(value, speedSmoke.colorKeys[0].time), sGradient.colorKeys[1] }, sGradient.alphaKeys); smokeColor.color = sGradient; }, smokeGradient.colorKeys[0].color, speedSmoke.colorKeys[0].color,speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { sGradient.SetKeys(new GradientColorKey[] { sGradient.colorKeys[0] , new GradientColorKey(value, speedSmoke.colorKeys[1].time) }, sGradient.alphaKeys); smokeColor.color = sGradient; }, smokeGradient.colorKeys[1].color, speedSmoke.colorKeys[1].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { smoke.colorOverLifetime.color.gradient.colorKeys[1].color = value; }, smokeGradient.colorKeys[1].color, speedSmoke.colorKeys[1].color,speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);

        LeanTween.value(gameObject, (Color value) => { lightTrail.startColor = value; }, lightGradient.colorKeys[0].color, speedLight.colorKeys[0].color,speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { darkTrail.startColor = value; }, darkGradient.colorKeys[0].color, speedDark.colorKeys[0].color,speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { lightTrail.endColor = value; }, lightGradient.colorKeys[1].color, speedLight.colorKeys[1].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { darkTrail.endColor = value; }, darkGradient.colorKeys[1].color, speedDark.colorKeys[1].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        
        yield return new WaitForSeconds(speedTime);

        LeanTween.value(gameObject, (Color value) => { sGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(value, smokeGradient.colorKeys[0].time), sGradient.colorKeys[1] }, sGradient.alphaKeys); smokeColor.color = sGradient; }, speedSmoke.colorKeys[0].color, smokeGradient.colorKeys[0].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { sGradient.SetKeys(new GradientColorKey[] { sGradient.colorKeys[0], new GradientColorKey(value, smokeGradient.colorKeys[1].time) }, sGradient.alphaKeys); smokeColor.color = sGradient; }, speedSmoke.colorKeys[1].color, smokeGradient.colorKeys[1].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);

        LeanTween.value(gameObject, (Color value) => { lightTrail.startColor = value; }, speedLight.colorKeys[0].color, lightGradient.colorKeys[0].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { darkTrail.startColor = value; }, speedDark.colorKeys[0].color, darkGradient.colorKeys[0].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc );
        LeanTween.value(gameObject, (Color value) => { lightTrail.endColor = value; }, speedLight.colorKeys[1].color, lightGradient.colorKeys[1].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(gameObject, (Color value) => { darkTrail.endColor = value; }, speedDark.colorKeys[1].color, darkGradient.colorKeys[1].color, speedColorTweenTime).setEase(LeanTweenType.easeOutCirc);
        
        playerMovementScript.move_speed -= speedIncrease;
        playerMovementScript.dash_speed -= speedIncrease;
        speedExec = false;
    }
}
