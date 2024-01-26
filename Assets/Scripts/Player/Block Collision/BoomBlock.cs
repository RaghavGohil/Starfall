using Game.Sound;
using UnityEngine;

internal sealed class BoomBlock: MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
        if(collision != null) 
        {
            if (collision.CompareTag("boomBlock"))
            {
                AudioManager.instance.PlayInGame("explosion");
                IDamage id = collision.GetComponent<IDamage>(); 
                if(id != null) 
                    id.Damage(100);
            }
        }
   }
}
