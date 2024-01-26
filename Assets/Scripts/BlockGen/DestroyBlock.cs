using System.Collections;
using Game.Sound;
using UnityEngine;

internal sealed class DestroyBlock : MonoBehaviour,IDie
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
        AudioManager.instance.PlayInGame("break block");
        yield return new WaitForSeconds(GetComponentInChildren<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}
