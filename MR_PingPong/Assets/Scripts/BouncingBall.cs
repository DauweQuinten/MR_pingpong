using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This component represents a ball that will bounce of a bouncy surface.
/// </summary>
public class BouncingBall : MonoBehaviour
{
    Rigidbody ballRb;
    Vector3 ballVelocity = Vector3.zero;

    private void Start()
    {
        ballRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ballVelocity = ballRb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out bouncySurface surface))
        {
            GameObject collisionObject = collision.gameObject;
            if (collisionObject != null)
            {
                Vector3 normal = collision.GetContact(0).normal;
                Vector3 velocityAfterBounce = Vector3.Reflect(ballVelocity, normal);
                ballRb.velocity = velocityAfterBounce * surface.GetBounciness();

                DebugDrawDirectionOfVector(collision.GetContact(0).point, normal, Color.blue, 1f);
                DebugDrawDirectionOfVector(collision.GetContact(0).point, ballVelocity, Color.green, 1f);
                DebugDrawDirectionOfVector(collision.GetContact(0).point, velocityAfterBounce, Color.red, 1f);
            }
        }
    }

    private void DebugDrawDirectionOfVector(Vector3 startPosition, Vector3 direction, Color color, float scale = float.PositiveInfinity)
    {
        Debug.DrawLine(startPosition, (startPosition + direction) * scale, color, float.PositiveInfinity);
    }
}
