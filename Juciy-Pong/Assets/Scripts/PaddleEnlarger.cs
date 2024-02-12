// This Code is inspired by: https://www.youtube.com/watch?v=PkNRPOrtyls

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaddleEnlarger : MonoBehaviour
{
    public PaddleMovement paddleLeft;
    public PaddleMovement paddleRight;
    private GameObject powerUp;

    void OnTriggerEnter(Collider other)
    {
        powerUp = GameObject.Find("SizePowerup");
        Destroy(powerUp);
        Debug.Log("Triggered!");

        paddleLeft.ChangePaddleSize(2.5f);
        paddleRight.ChangePaddleSize(2.5f);
    }
}

