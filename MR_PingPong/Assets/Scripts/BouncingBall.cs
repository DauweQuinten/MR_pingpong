using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    Rigidbody ballRb;
    bool hasBounced = false;

    Vector3 calculatedVelocity = Vector3.zero;
    Vector3 prevPos = Vector3.zero;
    Vector3 ballVelocity = Vector3.zero;
    float calculatedSpeed = 0f;

    private void Start()
    {
        ballRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //calculatedVelocity = (transform.position - prevPos) / Time.deltaTime;
        //calculatedSpeed =  calculatedVelocity.magnitude;
        //prevPos = transform.position;
        //Debug.Log($"Calculated speed: {calculatedSpeed}");

        ballVelocity = ballRb.velocity;
        Debug.Log($"Rigidbody speed: {ballVelocity.magnitude}");
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bouncy")
        {
            GameObject collisionObject = collision.gameObject;
            if (collisionObject != null)
            {
                if (collisionObject.TryGetComponent(out Rigidbody rb))
                {
                    // Calculate normal of collision
                    Vector3 normal = collision.GetContact(0).normal;
                    DebugDrawDirectionOfVector(collision.GetContact(0).point, normal, Color.blue, 1f);

                    // velocity vector of the ball BEFORE collision, calculated in fixed update
                    DebugDrawDirectionOfVector(collision.GetContact(0).point, ballVelocity, Color.green, 1f);

                    // Calculate the bounce vector and apply it to the ball rigidbody
                    Vector3 velocityAfterBounce = Vector3.Reflect(ballVelocity, normal);
                    DebugDrawDirectionOfVector(collision.GetContact(0).point, velocityAfterBounce, Color.red, 1f);

                    Debug.Log($"Magnitude of velocity after bouncing: {velocityAfterBounce.magnitude}");
                    ballRb.velocity = velocityAfterBounce;
                }    
            }
        }
    }

    private void DebugDrawDirectionOfVector(Vector3 startPosition, Vector3 direction, Color color, float scale = float.PositiveInfinity)
    {
        Debug.DrawLine(startPosition, (startPosition + direction) * scale, color, float.PositiveInfinity);
    }
}
