using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class DestroyBlock : MonoBehaviour
{
    ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void DestroyIt()
    {
        StartCoroutine(StartParticleSystem());
    }

    IEnumerator StartParticleSystem() 
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        CameraShaker.Instance.ShakeOnce(4f,4f,.1f,1f);
        particleSystem.Play();
        yield return new WaitForSeconds(particleSystem.duration);
        Destroy(gameObject);
    }
}
