using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Shootable
{
    [SerializeField]
    private GameObject leftShooter;
    [SerializeField]
    private GameObject rightShooter;

    public float fireRate; // how many projectiles in one second

    void ShootProjectile() 
    {
        
    }
}
