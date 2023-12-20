using Helper;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class ProjectilePool : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;

    static Stack<GameObject> projectilePool;
    static int numProjectiles;
    static int used;

    // Start is called before the first frame update
    void Start()
    {
        used = 0;
        numProjectiles = 100;
        projectilePool = new Stack<GameObject>();
        CreateProjectiles();
    }

    void CreateProjectiles()
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            GameObject newProjectile = Instantiate(projectile,transform);
            newProjectile.SetActive(false);
            projectilePool.Push(newProjectile);
        }
    }

    public static GameObject GetProjectile(Vector3 pos,Quaternion rotation) 
    {
        if (used < numProjectiles) 
        {
            GameObject projectile = projectilePool.Pop();
            projectile.SetActive(true);
            projectile.transform.position = pos;
            projectile.transform.rotation = rotation;
            used++;
            return projectile;
        }
        return null;
    }

    public static void ResetProjectile(GameObject p) 
    {
        p.SetActive(false);
        projectilePool.Push(p);
        used--;
    } 
}
