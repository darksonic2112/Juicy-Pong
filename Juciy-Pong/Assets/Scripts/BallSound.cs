using UnityEngine;
using UnityEngine.UI;

public class BallSound : MonoBehaviour
{
    public AudioClip paddleSound;
    public AudioClip wallSound;
    public AudioSource mainThemeAudioSource;

    void Start()
    {
        mainThemeAudioSource.pitch = 1f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            AudioSource.PlayClipAtPoint(paddleSound, transform.position);
            
        }
        else if (collision.gameObject.tag == "WallSurface")
        {
            AudioSource.PlayClipAtPoint(wallSound, transform.position);
        }
    }
    
    public void OnSpeedSliderValueChanged(float value)
    {
        mainThemeAudioSource.pitch = value;
    }
}