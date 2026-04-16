/// <summary>
/// TooltipBase
/// Author: Stanislav Rudenko
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>

using UnityEngine;
using System.Collections;

/// <summary>
/// Shared fade and screen-clamp behaviour for tooltip panels (requires <see cref="CanvasGroup"/> + <see cref="RectTransform"/>).
/// </summary>
public class TooltipBase : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    protected RectTransform rectTransform;

    public float fadeDuration = 0.15f;
    private Coroutine fadeCoroutine;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        Debug.Log(gameObject.name + " TooltipBase Awake - rectTransform null: " + (rectTransform == null));
        Debug.Log(gameObject.name + " TooltipBase Awake - canvasGroup null: " + (canvasGroup == null));
    }

    /// <summary>Fades alpha in and activates the GameObject.</summary>
    protected void FadeIn()
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

        gameObject.SetActive(true);
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(Fade(0f, 1f));
    }

    /// <summary>Fades out and deactivates when alpha reaches zero.</summary>
    public void Hide()
    {
        if (!gameObject.activeSelf)
            return;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float start, float end)
    {
        float time = 0f;
        canvasGroup.alpha = start;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = end;

        if (end == 0f)
            gameObject.SetActive(false);
    }

    /// <summary>Keeps the rect fully on-screen by shifting position.</summary>
    protected void ClampToScreen()
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        Vector3 offset = Vector3.zero;

        if (corners[0].x < 0)
            offset.x = -corners[0].x;

        if (corners[2].x > Screen.width)
            offset.x = Screen.width - corners[2].x;

        if (corners[0].y < 0)
            offset.y = -corners[0].y;

        if (corners[2].y > Screen.height)
            offset.y = Screen.height - corners[2].y;

        rectTransform.position += offset;
    }
}
