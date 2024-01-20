using TMPro;
using UnityEngine;

public class Health : MonoBehaviour,IDamage
{

    [SerializeField]
    int hp;

    // Start is called before the first frame update
    void Start()
    {
        SetText();
    }

    internal void SetText()
    {
        StatController.instance.SetHealthText(hp);
    }

    internal void AddHP(int amount)
    {
        hp += amount;
        hp = Mathf.Clamp(hp,0, 100);
        SetText();
    }

    public void Damage(int amount)
    {
        hp -= amount;
        hp = Mathf.Clamp(hp, 0, 100);
        SetText();
    }
}
