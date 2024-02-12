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
    public PaddleMovement paddleLeft;
    public PaddleMovement paddleRight;
    
    
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
        UpdateText(scoreLeft, scoreRight);
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
            UpdateText(scoreLeft, scoreRight);
            ResetBall(leftPaddle);
            paddleLeft.RevertPaddleSize();
            paddleRight.RevertPaddleSize();
            paddleLeft.RevertPaddleSpeed();
            paddleRight.RevertPaddleSpeed();
            CheckWinner();
        }
        else if (paddle == leftPaddle)
        {
            scoreLeft += 1;
            Debug.Log("Left Paddle just scored! The Score now is Left Paddle: " + scoreLeft + " to Right Paddle: " + scoreRight);
            UpdateText(scoreLeft, scoreRight);
            ResetBall(rightPaddle);
            paddleLeft.RevertPaddleSize();
            paddleRight.RevertPaddleSize();
            paddleLeft.RevertPaddleSpeed();
            paddleRight.RevertPaddleSpeed();
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
            UpdateText(scoreLeft, scoreRight);
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
            UpdateText(scoreLeft, scoreRight);
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

    public float GetBallSpeedZ()
    {
        return ballSpeed_z;
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
            ballSpeed_x *= -1f;
            Camera.main.GetComponent<CameraShake>().Shake();
        }
        
        if (collision.gameObject == leftPaddle)
        {
            //ReflectBallDirection(leftPaddle);
        }
        
        if (collision.gameObject == rightPaddle)
        {
           //ReflectBallDirection(rightPaddle);
        }
    }
    
    void UpdateText(float currentScoreLeft, float currentScoreRight)
    {
        if (currentScoreLeft == currentScoreRight)
        {
            Color textColorL = Color.white;
            Color textColorR = Color.white;
            leftScore.color = textColorL;
            rightScore.color = textColorR;
        }
        else if (currentScoreLeft > currentScoreRight)
        {
            float difference = currentScoreLeft - currentScoreRight;
            Color textColorL = new Color(0f, 0.78f + (difference / 50), 0f);
            Color textColorR = new Color(0.78f + (difference / 50), 0f, 0f);
            leftScore.color = textColorL;
            rightScore.color = textColorR;
        }
        else
        {
                float difference = currentScoreRight - currentScoreLeft;
                Color textColorL = new Color(0.78f + (difference / 50), 0f, 0f);
                Color textColorR = new Color(0f, 0.78f + (difference / 50), 0f);
                leftScore.color = textColorL;
                rightScore.color = textColorR;
        }
        
        leftScore.text = "Score: " + scoreLeft.ToString();
        rightScore.text = "Score: " + scoreRight.ToString();
    }
/*
    void ReflectBallDirection(GameObject paddle)
    {
        GameObject ball = GameObject.Find(ballName);

        // Get the center position of the paddle
        Vector3 paddleCenter = paddle.transform.position;
    
        // Calculate the direction from the paddle's center to the ball's position
        Vector3 reflectionDirection = ball.transform.position - paddleCenter;
    
        // Normalize the reflection direction
        reflectionDirection.Normalize();

        // Adjust the reflection direction based on the incoming angle
        float angle = Vector3.Angle(reflectionDirection, Vector3.right);
        float cappedAngle = Mathf.Clamp(angle, -60f, 60f);
        float ballSpeedMagnitude = Mathf.Sqrt(ballSpeed_x * ballSpeed_x + ballSpeed_z * ballSpeed_z);
        float newAngle = Mathf.Lerp(-60f, 60f, cappedAngle / 45); // Adjust the range of the new angle as needed
    
        // Calculate the new velocity components based on the new angle
        ballSpeed_x = Mathf.Cos(Mathf.Deg2Rad * newAngle) * ballSpeedMagnitude;
        ballSpeed_z = Mathf.Sin(Mathf.Deg2Rad * newAngle) * ballSpeedMagnitude;

        // Adjust the reflection direction based on the paddle's position
        if (ball.transform.position.y > paddle.transform.position.y)
        {
            // Ball hits upper half of the paddle
            reflectionDirection.y = Mathf.Abs(reflectionDirection.y);
        }
        else
        {
            // Ball hits lower half of the paddle
            reflectionDirection.y = -Mathf.Abs(reflectionDirection.y);
        }

        // Set the new ball velocity based on the reflection direction
        ballSpeed_x = reflectionDirection.x * ballSpeedMagnitude;
        ballSpeed_z = reflectionDirection.z * ballSpeedMagnitude;
    }
    */
}