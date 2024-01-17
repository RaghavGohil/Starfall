using UnityEngine;

internal sealed class PickCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin")) 
        {
            CoinManager.AddAmount(100);
            if (StatController.instance != null)
                StatController.instance.UpdateText();
            Destroy(collision.gameObject);
        }
    }
}
