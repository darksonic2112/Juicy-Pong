using UnityEngine;
using UnityEngine.UI;

public class BallSound : MonoBehaviour
{
    public AudioClip paddleSound;
    public AudioClip wallSound;
    public AudioSource mainThemeAudioSource;

    private float offset = 0f;

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

    public void SmoothPitchChange(float value)
    {
        offset = (value - 1f) / 10;
        while (mainThemeAudioSource.pitch > 1f)
        {
            mainThemeAudioSource.pitch -= offset;
            System.Threading.Thread.Sleep(25);
        }
    }
}