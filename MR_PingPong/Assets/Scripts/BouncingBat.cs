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
                    Vector3 normal = collision.GetContact(0).normal;
                    Vector3 reflectedVelocity = Vector3.Reflect(ballVelocity, normal);                   
                    //Vector3 BounceOfDirection = reflectedVelocity.normalized;
                    
                    //Vector3 bounceForce = (_velocity - ballVelocity) * _bounceMultiplier;
                    Vector3 bounceVelocity = _velocity + reflectedVelocity;
                    ballRb.velocity = Vector3.zero;
                    //ballRb.velocity = bounceVelocity;
                    ballRb.AddForceAtPosition(bounceVelocity, collision.GetContact(0).point, ForceMode.Impulse);
                    Debug.Log($"Velocity of the ball after bouncing: {ballRb.velocity}");

                    DebugDrawDirectionOfVector(collision.GetContact(0).point, reflectedVelocity.normalized, Color.blue, 1f);
                    DebugDrawDirectionOfVector(collision.GetContact(0).point, _velocity.normalized, Color.green, 1f);
                    DebugDrawDirectionOfVector(collision.GetContact(0).point, bounceVelocity.normalized, Color.red, 1f);

                    DebugDrawDirectionOfVector(collision.GetContact(0).point, ballRb.velocity.normalized, Color.cyan, 1f);
                }
            }
        }
    }

    private void DebugDrawDirectionOfVector(Vector3 startPosition, Vector3 direction, Color color, float size = float.PositiveInfinity)
    {   
        GameObject impactPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        impactPoint.transform.position = startPosition;
        impactPoint.transform.localScale *= 0.1f;
        Debug.DrawLine(startPosition, (startPosition + direction)*size, color, float.PositiveInfinity);
    }
}
