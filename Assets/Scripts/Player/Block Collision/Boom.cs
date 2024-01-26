using Game.Sound;
using System.Collections;
using UnityEngine;

internal sealed class Boom : MonoBehaviour,IDie 
{
    [SerializeField] float blastRadius;
    [SerializeField] ParticleSystem blastParticle;
    Collider2D col;
    bool isDead;
    private void Start()
    {
        isDead = false;
        col = GetComponent<Collider2D>();
    }
    public void DieInGame() 
    {
        blastParticle.Play();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,blastRadius);
        StartCoroutine(CineMachineCameraShaker.Instance.ShakeOnce(2f, 0.2f));
        if (colliders != null && colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (col != collider && !isDead)
                {
                    IDamage id = collider.GetComponent<IDamage>(); 
                    if(id != null) 
                    {
                        isDead = true;
                        id.Damage(100);
                    } 
                }
            }
        }
        StartCoroutine(StartParticleSystem());
    }
    internal IEnumerator StartParticleSystem() 
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<ParticleSystem>().Play();
        AudioManager.instance.PlayInGame("explosion");
        yield return new WaitForSeconds(GetComponentInChildren<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}
