// This Code is inspired by: https://www.youtube.com/watch?v=PkNRPOrtyls

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PaddleEnlarger : MonoBehaviour
{
    public PaddleMovement paddleLeft;
    public PaddleMovement paddleRight;
    public GameObject powerUpPrefab;
    private GameObject powerUp;

    private bool sizePowerUp = true;
    

    private void Update()
    {
        if (sizePowerUp)
        {
            int randomInt = Random.Range(1, 10000);
            if (randomInt == 50)
            {
                sizePowerUp = false;
                float randomFloat_x = Random.Range(-8f, 8f);
                float randomFloat_z = Random.Range(-20f, 20f);
                Vector3 randomPosition = new Vector3(randomFloat_x, 0, randomFloat_z);
                GameObject powerUp = GameObject.Find("SizePowerup");
                powerUp.transform.position = new Vector3(randomFloat_x, 0, randomFloat_z);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        sizePowerUp = true;
        powerUp = GameObject.Find("SizePowerup");
        powerUp.transform.position = new Vector3(20, 0, 0);
        Debug.Log("Triggered!");

        paddleLeft.ChangePaddleSize(2.5f);
        paddleRight.ChangePaddleSize(2.5f);
    }
}

