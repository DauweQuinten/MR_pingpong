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
                    Debug.LogWarning($"collision object: {collision.GetContact(0).thisCollider.gameObject.name}");
                    Debug.LogWarning($"collision other object: {collision.GetContact(0).otherCollider.gameObject.name}");

                    Vector3 normal = collision.GetContact(0).normal;
                    DebugDrawDirectionOfVector(collision.GetContact(0).point, normal, Color.blue, 1f);

                    Vector3 incommingBallVelocity = ballRb.velocity;
                    DebugDrawDirectionOfVector(collision.GetContact(0).point, incommingBallVelocity, Color.green, 1f);
                    Debug.Log($"Magnitude of incomming vector: {incommingBallVelocity.magnitude}");


                    Vector3 bouncingBallVelocity = Vector3.Reflect(incommingBallVelocity, normal);
                    DebugDrawDirectionOfVector(collision.GetContact(0).point, bouncingBallVelocity, Color.red, 1f);
                    Debug.Log($"Magnitude of outgoing vector: {bouncingBallVelocity.magnitude}");

                    ballRb.velocity = bouncingBallVelocity*2f;


                    //                    // **** TODO ****
                    //                    // De onderstaande berekening is vermoedelijk niet volledig correct volgens de wetten van de fysica.
                    //                    Vector3 ballVelocity = ballRb.velocity;
                    //                    Vector3 normal = collision.GetContact(0).normal;
                    //                    Vector3 reflectedVelocity = Vector3.Reflect(ballVelocity, normal);                   
                    //                    //Vector3 BounceOfDirection = reflectedVelocity.normalized;

                    //                    //Vector3 bounceForce = (_velocity - ballVelocity) * _bounceMultiplier;
                    //                    Vector3 bounceVelocity = _velocity + reflectedVelocity;
                    //                    ballRb.velocity = Vector3.zero;
                    //                    ballRb.AddForce(bounceVelocity, ForceMode.Impulse);
                    //                    //ballRb.velocity = bounceVelocity;
                    //                    Debug.Log($"Velocity of the ball after bouncing: {ballRb.velocity}");
                    //                    Debug.Log($"Velocity should be: {bounceVelocity}");

                    //#if UNITY_EDITOR

                    //                    DebugDrawDirectionOfVector(collision.GetContact(0).point, reflectedVelocity.normalized, Color.blue, 1f);
                    //                    DebugDrawDirectionOfVector(collision.GetContact(0).point, _velocity.normalized, Color.green, 1f);
                    //                    DebugDrawDirectionOfVector(collision.GetContact(0).point, bounceVelocity.normalized, Color.red, 1f);
                    //                    DebugDrawDirectionOfVector(collision.GetContact(0).point, ballRb.velocity.normalized, Color.cyan, 1.5f);
                    //#endif           

                    }
                }
            }
        }


        private void DebugDrawDirectionOfVector(Vector3 startPosition, Vector3 direction, Color color, float size = float.PositiveInfinity)
        {   
            Debug.DrawLine(startPosition, (startPosition + direction)*size, color, float.PositiveInfinity);
        }
}
