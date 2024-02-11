using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEditor.AnimatedValues;

public class BallMovement : MonoBehaviour
{
    public TextMeshProUGUI leftScore;
    public TextMeshProUGUI rightScore;
    public GameObject leftWinnerText;
    public GameObject rightWinnerText;
    public BallSound musicSpeed;
    public Material BlueMaterial;
    
    private float ballSpeed_x = 0.02f;
    private float ballSpeed_z = 0.02f;
    private float speedUp = 1f;
    private string ballName = "Ball";
    private string lineName = "Line (1)";
    private GameObject leftPaddle;
    private GameObject rightPaddle;
    private int scoreLeft = 0;
    private int scoreRight = 0;
    void Start()
    {
        leftPaddle = GameObject.Find("Paddle (left)");
        rightPaddle = GameObject.Find("Paddle (right)");
        UpdateText();
        leftWinnerText.SetActive(false);
        rightWinnerText.SetActive(false);
        ResetBall(leftPaddle);
        musicSpeed.OnSpeedSliderValueChanged(speedUp);
    }
    
    void Update()
    {
        GameObject ball = GameObject.Find(ballName);
        ball.transform.Translate(new Vector3(ballSpeed_x * speedUp, 0, ballSpeed_z * speedUp));
        Vector3 currentPosition = transform.position;
        
        if (ball.transform.position.z > 27)
        {
            Goal(rightPaddle);
        }

        else if (ball.transform.position.z < -27)
        {
            Goal(leftPaddle);
        }
    }

    void Goal(GameObject paddle)
    {
        if (paddle == rightPaddle)
        {
            scoreRight += 1;
            Debug.Log("Right Paddle just scored! The Score now is Left Paddle: " + scoreLeft + " to Right Paddle: " + scoreRight);
            UpdateText();
            ResetBall(leftPaddle);
            CheckWinner();
        }
        else if (paddle == leftPaddle)
        {
            scoreLeft += 1;
            Debug.Log("Left Paddle just scored! The Score now is Left Paddle: " + scoreLeft + " to Right Paddle: " + scoreRight);
            UpdateText();
            ResetBall(rightPaddle);
            CheckWinner();
        }
    }

    void CheckWinner()
    {
        if (scoreLeft >= 11)
        {
            GameObject ball = GameObject.Find(ballName);
            GameObject line = GameObject.Find(lineName);
            //line.SetActive(false);
            //ball.SetActive(false);
            //leftWinnerText.SetActive(true);
            Debug.Log("Left Paddle won!");
            scoreLeft = 0;
            scoreRight = 0;
            UpdateText();
        }
        
        else if (scoreRight >= 11)
        {
            GameObject ball = GameObject.Find(ballName);
            GameObject line = GameObject.Find(lineName);
            //line.SetActive(false);
            //ball.SetActive(false);
            //rightWinnerText.SetActive(true);
            Debug.Log("Right Paddle won!");
            scoreLeft = 0;
            scoreRight = 0;
            UpdateText();
        }
    }

    void ResetBall(GameObject serve)
    {
        if (speedUp > 1f)
        {   
            musicSpeed.SmoothPitchChange(speedUp);
        }
        speedUp = 1f;
        GameObject ball = GameObject.Find(ballName);
        float randomFloat_x = Random.Range(0.004f, 0.006f);
        
        ball.transform.position = Vector3.zero;
        ballSpeed_z = 0.02f;
        
        if (serve == rightPaddle)
        {
            ballSpeed_z = -Mathf.Abs(ballSpeed_z);
            ballSpeed_x *= randomFloat_x;
        }
        else if (serve == leftPaddle)
        {
            ballSpeed_x *= randomFloat_x;
        }
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            ballSpeed_z *= -1f;
            speedUp += 0.05f;
            musicSpeed.OnSpeedSliderValueChanged(speedUp);
            Camera.main.GetComponent<CameraShake>().Shake();
        }

        if (collision.gameObject.tag == "WallSurface")
        {
            ballSpeed_x *= -1.2f;
            Camera.main.GetComponent<CameraShake>().Shake();
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
    
    void UpdateText()
    {
        leftScore.text = "Score: " + scoreLeft.ToString();
        rightScore.text = "Score: " + scoreRight.ToString();
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