using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scrolling image for the Main Menu Background.
/// Author: Tin Trinh
/// Date: April 9, 2026
/// Source: Tutorial from https://www.youtube.com/watch?v=-6H-uYh80vc
/// </summary>
public class Scrolling : MonoBehaviour
{
    [SerializeField]
    private RawImage image;
    [SerializeField]
    private float x;
    [SerializeField]
    private float y;

    // Update is called once per frame
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(x, y) * Time.deltaTime, image.uvRect.size);
    }
}
