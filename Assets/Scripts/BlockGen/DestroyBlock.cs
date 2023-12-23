using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

internal sealed class DestroyBlock : MonoBehaviour
{

    public void DestroyIt()
    {
        StartCoroutine(StartParticleSystem());
    }

    IEnumerator StartParticleSystem() 
    {
        GetComponent<SpriteRenderer>().enabled = false;
        CameraShaker.Instance.ShakeOnce(4f,4f,.1f,1f);
        GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}
