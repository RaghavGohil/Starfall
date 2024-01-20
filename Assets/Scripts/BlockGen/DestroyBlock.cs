using System.Collections;
using UnityEngine;

internal sealed class DestroyBlock : MonoBehaviour
{

    public void DestroyIt()
    {
        StartCoroutine(StartParticleSystem());
    }

    IEnumerator StartParticleSystem() 
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<ParticleSystem>().Play();
        StartCoroutine(CineMachineCameraShaker.Instance.ShakeOnce(2f, 0.2f));
        yield return new WaitForSeconds(GetComponentInChildren<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}
