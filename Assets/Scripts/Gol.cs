using System;
using System.Collections;
using UnityEngine;

public class Gol : MonoBehaviour
{
    public static event Action<int> OnGolMarcado;
    
    [SerializeField] private int team;
    private GameObject _ball;
    private Vector3 _ballStartPos;
    private Rigidbody2D _ballRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject ballTagged = GameObject.FindGameObjectWithTag("Bola");
        if (ballTagged != null)
        {
            _ballRb = ballTagged.GetComponentInParent<Rigidbody2D>();
            _ball = _ballRb != null ? _ballRb.gameObject : ballTagged.transform.root.gameObject;
            _ballStartPos = _ball.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bola"))
        {
            OnGolMarcado?.Invoke(team);
            ResetBall();
        }
    }

    private void ResetBall()
    {
        if (_ball == null) return;

        if (_ballRb != null)
        {
            _ballRb.linearVelocity = Vector2.zero;
            _ballRb.angularVelocity = 0f;
            _ballRb.position = _ballStartPos;
            _ballRb.Sleep();
        }
        _ball.transform.position = _ballStartPos;
    }
}
