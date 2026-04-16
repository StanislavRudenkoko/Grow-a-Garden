/// <summary>
/// TestButton
/// Author: Stanislav Rudenko
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>

using UnityEngine;

/// <summary>
/// Minimal UI hook to verify button wiring in the Editor.
/// </summary>
public class TestButton : MonoBehaviour
{
    /// <summary>Logs when the bound button is clicked.</summary>
    public void OnClick()
    {
        Debug.Log("TEST BUTTON WORKS");
    }
}
