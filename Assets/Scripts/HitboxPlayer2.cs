using UnityEngine;

public class HitboxJogador2 : MonoBehaviour
{
    [SerializeField] private Player2 player;
    [SerializeField] private string tipoParte = "Pe";

    private void Awake()
    {
        if (player == null)
        {
            player = GetComponentInParent<Player2>();
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