using Game.Sound;
using UnityEngine;

public class DebrisBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null) 
        {
            if ((collision.CompareTag("debrisBlock") && GetComponent<PlayerMovement>().is_dashing == true) || GetComponent<SpeedBlock>().speedExec)
            { 
                AudioManager.instance.PlayInGame("break block");
                IDamage id = collision.GetComponent<IDamage>(); 
                if(id != null) 
                    id.Damage(100); 
            }
        }
    }
}