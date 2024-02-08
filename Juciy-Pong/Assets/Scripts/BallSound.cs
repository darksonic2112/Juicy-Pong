using UnityEngine;

public class BallSound : MonoBehaviour
{
    public AudioClip paddleSound;
    public AudioClip wallSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            audioSource.PlayOneShot(paddleSound);
        }
        else if (collision.gameObject.tag == "WallSurface")
        {
            audioSource.PlayOneShot(wallSound);
        }
    }
}