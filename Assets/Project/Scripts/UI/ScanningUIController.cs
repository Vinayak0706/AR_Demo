using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ScanningUIController : MonoBehaviour
{
    [Header("References")]
    public ARPlaneManager planeManager;
    public CanvasGroup scanningCanvas;
    public GameObject scanningText;
    public GameObject placeObjectText;

    bool hasFoundPlane = false;

    void Update()
    {
        if (hasFoundPlane)
            return;

        if (planeManager.trackables.count > 0)
        {
            hasFoundPlane = true;
            scanningText.SetActive(false);
            placeObjectText.SetActive(true);
        }
    }

    IEnumerator FadeOut()
    {
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            scanningCanvas.alpha = t;
            yield return null;
        }

    }

    IEnumerator FadeIn()
    {
        for (float t = 1f; t >= 0f; t -= Time.deltaTime)
        {
            scanningCanvas.alpha = t;
            yield return null;
        }
        scanningCanvas.alpha = 0f;

    }

}
