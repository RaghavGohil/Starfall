using Game.Sound;
using UnityEngine;

internal sealed class PickCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin")) 
        {
            CoinManager.AddAmount(10);
            AudioManager.instance.PlayInGame("coin");
            if (StatController.instance != null)
                StatController.instance.SetCoinText();
            Destroy(collision.gameObject);
        }
    }
}
