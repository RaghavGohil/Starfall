using Helper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
internal sealed class Projectile 
{
    public Vector2 dir;
    public GameObject obj;
    public float time;
    public Projectile(Vector2 dir,GameObject obj) 
    {
        this.dir = dir;
        this.obj = obj;
    }
}

internal sealed class Shoot : MonoBehaviour
{
    [SerializeField]
    GameObject leftShooter;
    [SerializeField]
    GameObject rightShooter;

    [SerializeField]
    List<Projectile> activeProjectiles;
    List<Projectile> removeList;

    [SerializeField]
    int fireRate; // how many projectiles in one second
    [SerializeField]
    float speed;
    float timeCount;
    float timeout;

    bool hasPressedFire;

    void Start()
    {
        timeCount = 1f;
        timeout = 5f;
        hasPressedFire = false;
        activeProjectiles = new List<Projectile>();
        removeList = new List<Projectile>();
    }

    public void FireDown()
    {
        hasPressedFire = true;
    }

    public void FireUp() 
    {
        timeCount = 1f;
        hasPressedFire = false;
    }

    void FixedUpdate() 
    {
        if (hasPressedFire)
        {
            timeCount += Time.fixedDeltaTime * fireRate;
            if (timeCount > 1f)
            {
                GameObject left = ProjectilePool.GetProjectile(leftShooter.transform.position, transform.rotation);
                GameObject right = ProjectilePool.GetProjectile(rightShooter.transform.position, transform.rotation);
                Debug.DrawRay(rightShooter.transform.position,rightShooter.transform.up,Color.blue);
                if (left != null && right != null) 
                {
                    activeProjectiles.Add(new Projectile(leftShooter.transform.up,left));
                    activeProjectiles.Add(new Projectile(rightShooter.transform.up,right));
                }
                timeCount = 0f;
            }
        }
    }

    void Update() 
    {
        TranslateProjectile();
    }

    public void TranslateProjectile() 
    {
        if (activeProjectiles.Count != 0) 
        {
            foreach (Projectile projectile in activeProjectiles) 
            {
                projectile.obj.transform.position += (Vector3)projectile.dir*Time.deltaTime*speed;
                projectile.time += Time.deltaTime;
                if (projectile.time > timeout) 
                { 
                    ProjectilePool.ResetProjectile(projectile.obj);
                    removeList.Add(projectile);
                }
                
            }
            foreach (Projectile projectile in removeList)
            {
                activeProjectiles.Remove(projectile);
            }
            removeList.Clear();
        }
    }
}
