using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    bool visible;

    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    RaycastHit2D raycastHit;
    const float raycastDistance = 1f;

    [SerializeField]
    string[] colliderTags;

    [SerializeField]
    float rotateAmount;

    [SerializeField]
    GameObject trails;

    void Start()
    {
        visible = false;
        boxCollider = GetComponent<BoxCollider2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        trails.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.up * 0.5f * Time.deltaTime;
        if (visible) 
        {
            raycastHit = Physics2D.Raycast(transform.position,transform.up,raycastDistance);
            if (raycastHit.collider!= null) 
            {
                foreach (string tag in colliderTags)
                {
                    if (tag == raycastHit.collider.tag)
                    {
                        print("oh");
                        transform.Rotate(new Vector3(0f, 0f, rotateAmount*Time.fixedDeltaTime));
                        break;
                    }
                }
            }
        }
        
    }

    void OnBecameInvisible()
    {
        boxCollider.enabled = false;
        visible = false;
        trails.SetActive(false);
    }

    void OnBecameVisible()
    {
        visible=true;
        boxCollider.enabled = true;
        trails.SetActive(true);
    }
}
