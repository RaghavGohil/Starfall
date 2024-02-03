using System.Collections;
using Game.Sound;
using UnityEngine;

internal sealed class FirePowerBlock : MonoBehaviour
{
    [SerializeField] GameObject shooters;
    [SerializeField] int maxFireRate;
    [SerializeField] int minFireRate;
    [SerializeField] float time;
    [HideInInspector] public StatusText statusTextScript;
    Shoot[] shootInstances;

    private void Start()
    {
        shootInstances = shooters.GetComponentsInChildren<Shoot>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null) 
        {
            //if (collision.CompareTag("firePowerBlock") && (GetComponent<PlayerMovement>().is_dashing == true || GetComponent<SpeedBlock>().speedExec))
            if (collision.CompareTag("firePowerBlock"))
            { 
                AudioManager.instance.PlayInGame("powerup");
                IDamage id = collision.GetComponent<IDamage>();
                if(id != null ) 
                    collision.GetComponent<IDamage>().Damage(100);
                StartCoroutine(PowerUp());
            }
        }
    }
    IEnumerator PowerUp() 
    {
        for(int i = 0; i < shootInstances.Length; i++) 
        {
            shootInstances[i].fireRate = maxFireRate;
        }
        StartCoroutine(statusTextScript.StartAnimation("FIREPOWER +"));
        yield return new WaitForSeconds(time);
        for(int i = 0; i < shootInstances.Length; i++) 
        {
            shootInstances[i].fireRate = minFireRate;
        }  
    }
}
