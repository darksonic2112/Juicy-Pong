using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PaddleMovement : MonoBehaviour
{
    float moveSpeed = 15f;
    public string leftPaddleAxis = "Vertical";
    public string rightPaddleAxis = "Horizontal";
    public float lowerWallEnd = -9.4f;
    public float higherWallEnd = 9.4f;
    public BallMovement movement;
    public bool debuffLeft = false;
    public bool debuffRight = false;

    private void Start()
    {
        GameObject paddleLeft = GameObject.Find("Paddle (left)");
        GameObject paddleRight = GameObject.Find("Paddle (right)");
        paddleLeft.transform.localScale = new Vector3(5f, 1f, 1f);
        paddleRight.transform.localScale = new Vector3(5f, 1f, 1f);
    }

    void Update()
    {
        float leftPaddleInput = Input.GetAxis(leftPaddleAxis);
        MovePaddle(leftPaddleInput, "Paddle (left)");
        
        float rightPaddleInput = Input.GetAxis(rightPaddleAxis);
        MovePaddle(rightPaddleInput, "Paddle (right)");
    }

    void MovePaddle(float input, string paddleName)
    {
        GameObject paddle = GameObject.Find(paddleName);
        if (debuffLeft && paddleName == "Paddle (left)")
        {
            float moveAmount = input * moveSpeed / 2 * Time.deltaTime;
            if (paddle != null && (paddle.transform.position.x >= lowerWallEnd && paddle.transform.position.x <= higherWallEnd))
            {
                paddle.transform.Translate(new Vector3(moveAmount, 0, 0));
            }
        }
        else if (debuffRight && paddleName == "Paddle (right)")
        {
            float moveAmount = input * moveSpeed / 2 * Time.deltaTime;
            if (paddle != null && (paddle.transform.position.x >= lowerWallEnd && paddle.transform.position.x <= higherWallEnd))
            {
                paddle.transform.Translate(new Vector3(moveAmount, 0, 0));
            }
        }
        else
        {
            float moveAmount = input * moveSpeed * Time.deltaTime;
            if (paddle != null && (paddle.transform.position.x >= lowerWallEnd && paddle.transform.position.x <= higherWallEnd))
            {
                paddle.transform.Translate(new Vector3(moveAmount, 0, 0));
            }
        }

        if (paddle != null && paddle.transform.position.x <= lowerWallEnd)
        {
            paddle.transform.Translate(new Vector3(higherWallEnd - 1, 0, 0));
        }
        else if (paddle != null && paddle.transform.position.x >= higherWallEnd)
        {
            paddle.transform.Translate(new Vector3(lowerWallEnd + 1, 0, 0));
        }
        
        if ((paddleName == "Paddle (left)" && Input.GetKey(KeyCode.D)) || paddleName == "Paddle (right)" && Input.GetKey(KeyCode.RightArrow))
        {
            Quaternion currentRotation = paddle.transform.rotation;
            float rotationAmount = 0.01f;
            paddle.transform.rotation = currentRotation * Quaternion.Euler(0f, rotationAmount, 0f);
        }
        if ((paddleName == "Paddle (left)" && Input.GetKey(KeyCode.A)) || paddleName == "Paddle (right)" && Input.GetKey(KeyCode.LeftArrow))
        {
            Quaternion currentRotation = paddle.transform.rotation;
            float rotationAmount = -0.01f;
            paddle.transform.rotation = currentRotation * Quaternion.Euler(0f, rotationAmount, 0f);
        }
    }

    public void ChangePaddleSize(float amount)
    {
        GameObject paddleLeft = GameObject.Find("Paddle (left)");
        GameObject paddleRight = GameObject.Find("Paddle (right)");
        float ballDirectionZ = movement.GetBallSpeedZ();
        if (ballDirectionZ < 0)
        {
            paddleLeft.transform.localScale = new Vector3(5f + amount, 1f, 1f);
            paddleRight.transform.localScale = new Vector3(5f - amount, 1f, 1f);
        }
        else
        {
            paddleLeft.transform.localScale = new Vector3(5f - amount, 1f, 1f);
            paddleRight.transform.localScale = new Vector3(5f + amount, 1f, 1f);
        }
    }

    public void ChangePaddleSpeed()
    {
        float ballDirectionZ = movement.GetBallSpeedZ();
        if (ballDirectionZ > 0)
        {
            debuffLeft = true;
        }
        else
        {
            debuffRight = true;
        }
    }

    public void RevertPaddleSize()
    {
        GameObject paddleLeft = GameObject.Find("Paddle (left)");
        GameObject paddleRight = GameObject.Find("Paddle (right)");
        paddleLeft.transform.localScale = new Vector3(5f, 1f, 1f);
        paddleRight.transform.localScale = new Vector3(5f, 1f, 1f);
    }

    public void RevertPaddleSpeed()
    {
        debuffLeft = false;
        debuffRight = false;
    }
    
}