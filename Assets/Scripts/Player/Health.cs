using Game.Sound;
using UnityEngine;

public class Health : MonoBehaviour,IDamage
{

    [SerializeField]
    int hp;
    [SerializeField] ParticleSystem[] deathParticles;
    [SerializeField] GameObject[] disableObjects;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float colorTweenTime;
    Color originalColor;

    [SerializeField] bool canSetHealthText;

    public enum State 
    {
        Alive,
        Dead
    };

    public State aliveStatus { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        aliveStatus = State.Alive;
        originalColor = spriteRenderer.color;
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
        AudioManager.instance.PlayInGame("bullet hit");
        SetText();
        if (hp <= 0 && aliveStatus == State.Alive) 
        {
            aliveStatus = State.Dead;
            PlayDeathParticlesOnDeath();
            DisableGameObjectsOnDeath();
            GetComponent<IDie>().DieInGame();
            return;
        }
        spriteRenderer.color = Color.white;
        LeanTween.value(gameObject, (value) => { spriteRenderer.color = value; }, originalColor, Color.red, colorTweenTime).setLoopPingPong(1);
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
