using System.Collections;
using Game.Sound;
using UnityEngine;

internal sealed class HeartBlock : MonoBehaviour
{
    [HideInInspector] public StatusText statusTextScript;
    Health healthInstance;

    private void Start()
    {
        healthInstance = GetComponent<Health>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null) 
        {
            //if (collision.CompareTag("firePowerBlock") && (GetComponent<PlayerMovement>().is_dashing == true || GetComponent<SpeedBlock>().speedExec))
            if (collision.CompareTag("heartBlock"))
            { 
                AudioManager.instance.PlayInGame("powerup");
                IDamage id = collision.GetComponent<IDamage>();
                if(id != null ) 
                    collision.GetComponent<IDamage>().Damage(100);
                PowerUp();
            }
        }
    }
    void PowerUp() 
    {
        StartCoroutine(statusTextScript.StartAnimation("HEALTH +"));
        healthInstance.AddHP(30);
    }
}
