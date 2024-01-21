using UnityEngine;

public class Health : MonoBehaviour,IDamage
{

    [SerializeField]
    int hp;
    [SerializeField] ParticleSystem[] deathParticles;
    [SerializeField] GameObject[] disableObjects;

    [SerializeField] bool canSetHealthText;

    // Start is called before the first frame update
    void Start()
    {
        SetText();
    }

    internal void SetText()
    {
        if(canSetHealthText && hp>=0)
            StatController.instance.SetHealthText(hp);
    }

    internal void AddHP(int amount)
    {
        hp += amount;
        hp = Mathf.Clamp(hp,0, 100);
        SetText();
    }
    public int GetHP() 
    {
        return hp; 
    }

    public void Damage(int amount)
    {
        hp -= amount;
        hp = Mathf.Clamp(hp, 0, 100);
        
        SetText();

        if (hp <= 0) 
        {
            PlayDeathParticlesOnDeath();
            DisableGameObjectsOnDeath();
            GetComponent<IDie>().DieInGame();
        }
    }
    
    

    internal void PlayDeathParticlesOnDeath() 
    {
        if (deathParticles != null && deathParticles.Length != 0) 
        {
            for (int i = 0; i < deathParticles.Length; i++)
            {
                deathParticles[i].Play(); 
            }
        }
    }
    internal void DisableGameObjectsOnDeath() 
    {
        if (disableObjects != null && disableObjects.Length != 0) 
        {
            for (int i = 0; i < disableObjects.Length; i++)
            {
                disableObjects[i].SetActive(false);
            }
        }
    }
    
}
