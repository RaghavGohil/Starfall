using UnityEngine;

public abstract class Shootable : MonoBehaviour
{
    void ShootProjectile() 
    {
        GetComponent<Shoot>().ShootProjectile();
    }
}
