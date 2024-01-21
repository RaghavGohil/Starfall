using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEnemy : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (IsOnLayer(collision.gameObject,layerMask))
            {
                DieInGame();
            }
        }
    }

    public void DieInGame() 
    {
        Destroy(GetComponent<EnemyAI>());
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<IDamage>().Damage(100);
    }

    bool IsOnLayer(GameObject obj, LayerMask layerMask)
    {
        // Check if the GameObject's layer is in the LayerMask
        return layerMask == (layerMask | (1 << obj.layer));
    } 
}
