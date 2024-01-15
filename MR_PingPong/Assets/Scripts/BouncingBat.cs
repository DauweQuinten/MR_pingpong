using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBat : MonoBehaviour
{
    private Rigidbody _batRb;
    private float _bounceMultiplier = 1.5f;
    private Vector3 _velocity = Vector3.zero; 
    private Vector3 prevPos = Vector3.zero;

    private void Start()
    {
        _batRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _velocity = (transform.position - prevPos) / Time.deltaTime;
        prevPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameObject ball = collision.gameObject;
            if (ball != null)
            {
                if(ball.TryGetComponent(out Rigidbody ballRb))
                {
                    // **** TODO ****
                    // De onderstaande berekening is vermoedelijk niet volledig correct volgens de wetten van de fysica.

                    
                    Vector3 ballVelocity = ballRb.velocity;
                    //Vector3 bounceForce = (_velocity - ballVelocity) * _bounceMultiplier;
                    Vector3 bounceForce = _velocity * _bounceMultiplier;

                    ballRb.velocity = bounceForce;
                    //ballRb.AddForce(bounceForce, ForceMode.Impulse);
                    Debug.Log($"Added force to ball: {bounceForce}");
                }
            }
        }
    }
}
