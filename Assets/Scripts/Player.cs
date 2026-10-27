using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private float _xDirection;
    [SerializeField] private float xSpeed = 8f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float kickForce = 15f;
    [SerializeField] private float headForce = 12f;

    private Animator _playerAnimator;
    private Rigidbody2D _ballRb;
    private bool onJumpCount = false;
    private bool isTouchingBall = false;
    private string parteContato = ""; // Guarda "Pe" ou "Cabeca"

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponentInChildren<Animator>();
    }

    void OnMove(InputValue input)
    {
        _xDirection = input.Get<Vector2>().x;
    }

    void FlipSprite()
    {
        float sinal = Math.Sign(_playerRb.linearVelocityX);
        if (sinal != 0)
        {
            transform.localScale = new Vector3(sinal, 1, 1);
        }
    }

    void Movimentar()
    {
        _playerRb.linearVelocityX = _xDirection * xSpeed;
        bool isRunning = Mathf.Abs(_playerRb.linearVelocityX) > 0.05f;
        _playerAnimator.SetBool("IsRunning", isRunning);
        if (isRunning)
        {
            FlipSprite();
        }
    }

    private void FixedUpdate()
    {
        Movimentar();
    }

    void OnJump()
    {
        if (onJumpCount) return;

        onJumpCount = true;
        _playerRb.linearVelocityY = 0;
        _playerRb.AddForceY(jumpForce, ForceMode2D.Impulse);
        _playerAnimator.SetBool("IsJump", true);
    }

    void OnAttack()
    {
        if (isTouchingBall && _ballRb != null && parteContato == "Pe")
        {
            _ballRb.linearVelocity = Vector2.zero;
            Vector2 direcaoChute = new Vector2(transform.localScale.x, 0.4f).normalized;
            _ballRb.AddForce(direcaoChute * kickForce, ForceMode2D.Impulse);
        }

        _playerAnimator.SetTrigger("IsKick");
    }
    
    private void ExecutarCabecada()
    {
        if (_ballRb != null)
        {
            // Zera o impulso anterior da bola para a cabeçada responder imediatamente
            _ballRb.linearVelocity = Vector2.zero;

            // Impulsiona na direção em que o jogador está virado
            Vector2 direcaoCabecada = new Vector2(transform.localScale.x, 0.2f).normalized;
            _ballRb.AddForce(direcaoCabecada * headForce, ForceMode2D.Impulse);
        }
    }

    public void RegistrarContatoBola(Rigidbody2D ballRb, string parte)
    {
        _ballRb = ballRb;
        isTouchingBall = true;
        parteContato = parte;
        
        if (parte == "Cabeca" && onJumpCount)
        {
            ExecutarCabecada();
        }
    }

    public void LimparContatoBola(string parte)
    {
        if (parteContato == parte)
        {
            isTouchingBall = false;
            _ballRb = null;
            parteContato = "";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ColisaoChao"))
        {
            _playerAnimator.SetBool("IsJump", false);
            onJumpCount = false;
        }
    }
}