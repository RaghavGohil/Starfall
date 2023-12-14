using UnityEngine;
using UnityEngine.SceneManagement;

internal sealed class BlockCollision : MonoBehaviour
{
    PlayerMovement playerMovementScript;

    void Start() => playerMovementScript = gameObject.GetComponent<PlayerMovement>();

    void OnCollisionEnter2D(Collision2D collision)
    {

        switch (collision.transform.tag) 
        {
            case "blocks":
                if(!playerMovementScript.is_dashing)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                else
                    collision.transform.GetComponent<DestroyBlock>().DestroyIt();
                break;
        }
    }
}
