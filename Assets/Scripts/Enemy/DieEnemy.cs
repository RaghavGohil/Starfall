using Game.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEnemy : MonoBehaviour,IDie
{
    [SerializeField] LayerMask layerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (IsOnLayer(collision.gameObject,layerMask))
            {
                GetComponent<IDamage>().Damage(100);
            }
        }
    }

    public void DieInGame() 
    {
        Destroy(GetComponent<EnemyAI>());
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        StatController.instance.sceneScoreAmount += 20;
        StatController.instance.SetScoreText();
        AudioManager.instance.PlayInGame("die");
        GetComponent<Drop>().DropStuff();
        StartCoroutine(CineMachineCameraShaker.Instance.ShakeOnce(2f, 0.2f));
    }

    bool IsOnLayer(GameObject obj, LayerMask layerMask)
    {
        // Check if the GameObject's layer is in the LayerMask
        return layerMask == (layerMask | (1 << obj.layer));
    } 
}
