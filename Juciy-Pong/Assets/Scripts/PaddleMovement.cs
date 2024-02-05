using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    
    public float paddleSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Retrieve horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate movement direction
        Vector3 moveDirection = Vector3.right * horizontalInput;

        // Move the paddle
        transform.Translate(moveDirection * paddleSpeed * Time.deltaTime);
    }
}