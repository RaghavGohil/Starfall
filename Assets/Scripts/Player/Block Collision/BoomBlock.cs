using UnityEngine;

internal sealed class BoomBlock: MonoBehaviour
{
    [HideInInspector] public StatusText statusTextScript;
   private void OnTriggerEnter2D(Collider2D collision)
   {
        if(collision != null) 
        {
            if (collision.CompareTag("boomBlock"))
            { 
                IDamage id = collision.GetComponent<IDamage>(); 
                if(id != null) 
                    id.Damage(100);
                StartCoroutine(statusTextScript.StartAnimation("BOOM!!!"));
            }
        }
   }
}
