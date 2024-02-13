using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 1f;
    public float shakeAmount = 1f;
    public float shakeSpeed = 0.5f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float noiseX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0);
            float noiseY = Mathf.PerlinNoise(0, Time.time * shakeSpeed);
            
            float offsetX = Mathf.Lerp(-shakeAmount, shakeAmount, noiseX);
            float offsetY = Mathf.Lerp(-shakeAmount, shakeAmount, noiseY);
            
            transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f);
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        
        transform.localPosition = originalPosition;
    }
}
