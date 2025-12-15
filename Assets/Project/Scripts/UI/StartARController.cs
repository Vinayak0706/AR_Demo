using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections;

public class StartARController : MonoBehaviour
{
    [Header("UI")]
    public GameObject startScreen;
    public CanvasGroup fadeOverlay;
    public ARCameraBackground arCameraBackground;
    public CanvasGroup scanningCanvas;

    [Header("AR")]
    public ARSession arSession;

    void Start()
    {
        // Disable AR tracking 
        arSession.enabled = false;
        arCameraBackground.enabled = false;

        fadeOverlay.alpha = 1f;
        StartCoroutine(FadeIn());
    }

    public void OnStartButtonPressed()
    {
        StartCoroutine(StartARSequence());
    }

    IEnumerator StartARSequence()
    {
        yield return StartCoroutine(FadeOut());

        startScreen.SetActive(false);

        // Start AR tracking
        arSession.enabled = true;
        arCameraBackground.enabled = true;

        yield return StartCoroutine(FadeIn());
        scanningCanvas.alpha = 1f;
    }

    IEnumerator FadeIn()
    {
        for (float t = 1f; t >= 0f; t -= Time.deltaTime)
        {
            fadeOverlay.alpha = t;
            yield return null;
        }
        fadeOverlay.alpha = 0f;
        
    }

    IEnumerator FadeOut()
    {
        for (float t = 0f; t <= 1f; t += Time.deltaTime)
        {
            fadeOverlay.alpha = t;
            yield return null;
        }
        fadeOverlay.alpha = 1f;
    }
}
