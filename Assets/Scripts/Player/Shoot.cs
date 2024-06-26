using Game.Sound;
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
    public Projectile(Vector2 dir,GameObject obj) 
    {
        this.dir = dir;
        this.obj = obj;
    }
}

internal sealed class Shoot : MonoBehaviour
{
    GameObject shooter;

    [SerializeField] static List<Projectile> activeProjectiles;

    public int fireRate; // how many projectiles in one second
    float timeCount;

    bool hasPressedFire;

    void Start()
    {
        timeCount = 1f;
        hasPressedFire = false;
        activeProjectiles = new List<Projectile>();
        shooter = transform.gameObject;
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
            Debug.DrawRay(shooter.transform.position,shooter.transform.up,Color.blue);
            timeCount += Time.fixedDeltaTime * fireRate;
            if (timeCount > 1f)
            {
                AudioManager.instance.PlayInGame("shoot");
                GameObject _shooter = ProjectilePool.GetProjectile(shooter.transform.position, transform.rotation);
                if (_shooter != null) 
                {
                    Projectile p = new Projectile(shooter.transform.up, _shooter);
                    activeProjectiles.Add(p);
                }
                timeCount = 0f;
            }
        }
    }

    void Update() 
    {
        ReplaceProjectiles();
    }

    public void ReplaceProjectiles() 
    {
        if (activeProjectiles.Count != 0) 
        {
            /**foreach (Projectile projectile in activeProjectiles) 
            {
                if (projectile.obj.GetComponent<Bullet>().isDone) 
                { 
                    removeList.Add(projectile);
                    projectile.obj.GetComponent<Bullet>().isDone = false;
                }
                
            }
            foreach (Projectile projectile in removeList)
            {
                activeProjectiles.Remove(projectile);
                ProjectilePool.ResetProjectile(projectile.obj);
            }
            removeList.Clear();**/
            foreach (Projectile projectile in activeProjectiles.ToList()) 
            {
                if (projectile.obj.GetComponent<Bullet>().isDone) 
                { 
                    ProjectilePool.ResetProjectile(projectile.obj);
                    activeProjectiles.Remove(projectile);
                    projectile.obj.GetComponent<Bullet>().isDone = false;
                }
            }
        }
    }
}
