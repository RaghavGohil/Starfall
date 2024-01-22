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
        GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(GetComponentInChildren<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}
