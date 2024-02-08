using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Canvas leftScore;
    public Canvas rightScore;
    
    private float ballSpeed_x = 0.02f;
    private float ballSpeed_z = 0.02f;
    private float speedUp = 1f;
    private string ballName = "Ball"; 
    private GameObject leftPaddle;
    private GameObject rightPaddle;
    private Vector3 previousPosition;
    private int scoreLeft = 0;
    private int scoreRight = 0;
    void Start()
    {
        leftPaddle = GameObject.Find("Paddle (left)");
        rightPaddle = GameObject.Find("Paddle (right)");
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject ball = GameObject.Find(ballName);
        ball.transform.Translate(new Vector3(ballSpeed_x * speedUp, 0, ballSpeed_z * speedUp));
        Vector3 currentPosition = transform.position;
        
        if (ball.transform.position.z > 27)
        {
            Goal(rightPaddle);
        }

        if (ball.transform.position.z < -27)
        {
            Goal(leftPaddle);
        }
    }

    void Goal(GameObject paddle)
    {
        if (paddle == rightPaddle)
        {
            scoreRight += 1;
            ResetBall(leftPaddle);
        }
        else if (paddle == leftPaddle)
        {
            scoreLeft += 1;
            ResetBall(rightPaddle);
        }
    }

    void Counter()
    {
        
    }

    void ResetBall(GameObject serve)
    {
        speedUp = 1f;
        GameObject ball = GameObject.Find(ballName);
        float randomFloat_x = Random.Range(0.004f, 0.006f);
    
        ball.transform.position = Vector3.zero;
        ballSpeed_z = 0.02f;
    
        if (serve == rightPaddle)
        {
            ballSpeed_z = -Mathf.Abs(ballSpeed_z);
            ballSpeed_x = randomFloat_x;
            ball.transform.position = rightPaddle.transform.position;
        }
        else if (serve == leftPaddle)
        {
            ballSpeed_x *= randomFloat_x;
            ball.transform.position = leftPaddle.transform.position;
        }
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            ballSpeed_z *= -1f;
            speedUp += 0.1f;
        }

        if (collision.gameObject.tag == "WallSurface")
        {
            ballSpeed_x *= -1.01f;
        }
        
        if (collision.gameObject == leftPaddle)
        {
            ReflectBallDirection(leftPaddle);
        }
        
        if (collision.gameObject == rightPaddle)
        {
            ReflectBallDirection(rightPaddle);
        }
    }
    
    void BallSpeed()
    {
        
    }

    void ReflectBallDirection(GameObject paddle)
    {
        GameObject ball = GameObject.Find(ballName);

        float paddleHeight = paddle.transform.localScale.y;
        float upperHalfHeight = paddleHeight / 2.0f;
        float lowerHalfHeight = paddleHeight / 2.0f;
    
        Vector3 upperHalfPosition = paddle.transform.position + Vector3.up * upperHalfHeight;
        Vector3 lowerHalfPosition = paddle.transform.position - Vector3.up * lowerHalfHeight;
        
        if (ball.transform.position.y > upperHalfPosition.y)
        {
            ballSpeed_z = Mathf.Abs(ballSpeed_z);
        }
        
        else if (ball.transform.position.y < lowerHalfPosition.y)
        {
            ballSpeed_z = -Mathf.Abs(ballSpeed_z);
        }

        if (Mathf.Abs(ballSpeed_x) < 0.1 && ball.transform.position.x > paddle.transform.position.x)
        {
            ballSpeed_x = 0.02f;
        }
        else if (ball.transform.position.x > paddle.transform.position.x)
        {
            ballSpeed_x = Mathf.Abs(ballSpeed_x);
        }
        else if (Mathf.Abs(ballSpeed_x) < 0.1 && ball.transform.position.x < paddle.transform.position.x)
        {
            ballSpeed_x = -0.02f;
        }
        else
        {
            ballSpeed_x = -Mathf.Abs(ballSpeed_x);
        }
    }
}