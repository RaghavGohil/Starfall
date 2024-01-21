/* if anything is colliding to the object, it dies */
using UnityEngine;

internal sealed class DiePlayer : MonoBehaviour,IDie
{
    [HideInInspector] public GameObject loseScreen;
    [SerializeField] LayerMask layerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (IsOnLayer(collision.gameObject,layerMask) && GetComponent<PlayerMovement>().is_dashing == false)
            {
                GetComponent<IDamage>().Damage(100);
                DieInGame();
            }
        }
    }

    public void DieInGame() 
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
        loseScreen.SetActive(true);
        StartCoroutine(CineMachineCameraShaker.Instance.ShakeOnce(2f, 0.2f));
    }

    bool IsOnLayer(GameObject obj, LayerMask layerMask)
    {
        // Check if the GameObject's layer is in the LayerMask
        return layerMask == (layerMask | (1 << obj.layer));
    }
}
