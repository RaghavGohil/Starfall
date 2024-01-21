using UnityEngine;

internal sealed class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float time;
    [SerializeField] int damageAmount;
    [HideInInspector] public bool isDone;
    [SerializeField] LayerMask layerMask;
    float timeElapsed;
    private void Start()
    {
        isDone = false;
    }
    private void FixedUpdate()
    {
        transform.position += transform.up * bulletSpeed * Time.fixedDeltaTime;        
        timeElapsed += Time.fixedDeltaTime;
        if (timeElapsed > time)
        {
            isDone = true;
            timeElapsed = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) 
        {
            print(collision.name);
            if (IsOnLayer(collision.gameObject, layerMask))
            { 
                IDamage damageInstance = collision.GetComponent<IDamage>();
                if (damageInstance != null)
                {
                    damageInstance.Damage(damageAmount);
                }
                isDone = true;
            }
        } 
    }
    bool IsOnLayer(GameObject obj, LayerMask layerMask)
    {
        // Check if the GameObject's layer is in the LayerMask
        return layerMask == (layerMask | (1 << obj.layer));
    }
}
