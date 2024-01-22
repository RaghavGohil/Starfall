using System.Collections;
using UnityEngine;

internal class DestroyBlock : MonoBehaviour,IDie
{

    public void DieInGame()
    {
        StartCoroutine(StartParticleSystem());
    }

    internal IEnumerator StartParticleSystem() 
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(CineMachineCameraShaker.Instance.ShakeOnce(2f, 0.2f));
        GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(GetComponentInChildren<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}
