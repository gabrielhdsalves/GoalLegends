using UnityEngine;

public class HitboxJogador : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private string tipoParte = "Pe";

    private void Awake()
    {
        if (player == null)
        {
            player = GetComponentInParent<Player>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bola"))
        {
            Rigidbody2D ballRb = collision.GetComponentInParent<Rigidbody2D>();
            if (ballRb != null)
            {
                player.RegistrarContatoBola(ballRb, tipoParte);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bola"))
        {
            player.LimparContatoBola(tipoParte);
        }
    }
}