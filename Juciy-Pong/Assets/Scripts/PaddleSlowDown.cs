using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleSlowDown : MonoBehaviour
{
    public PaddleMovement paddleLeft;
    public PaddleMovement paddleRight;
    private bool slowDown = true;
    private GameObject slowUp;
    public GameObject powerUpPrefab;

    private void Update()
    {
        if (slowDown)
        {
            int randomInt = Random.Range(1, 10000);
            if (randomInt == 100)
            {
                slowDown = false;
                float randomFloat_x = Random.Range(-8f, 8f);
                float randomFloat_z = Random.Range(-20f, 20f);
                Vector3 randomPosition = new Vector3(randomFloat_x, 0, randomFloat_z);
                GameObject slowUp = GameObject.Find("SlowDown");
                slowUp.transform.position = new Vector3(randomFloat_x, 0, randomFloat_z);
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        slowDown = true;
        slowUp = GameObject.Find("SlowDown");
        slowUp.transform.position = new Vector3(20, 0, 0);
        Debug.Log("Triggered!");

        paddleLeft.ChangePaddleSpeed();
        paddleRight.ChangePaddleSpeed();
    }
}

