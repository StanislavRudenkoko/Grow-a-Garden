/// <summary>
/// HowToPlayButton
/// Author: Stanislav Rudenko
/// Date: Apr. 17, 2026
/// Source: with help of Claude AI
/// </summary>

using UnityEngine;

/// <summary>
/// UI control that shows or hides the “how to play” panel when invoked (e.g. from a button).
/// </summary>
public class HowToPlayButton : MonoBehaviour
{
    /// <summary>Panel GameObject toggled by <see cref="TogglePanel"/>.</summary>
    public GameObject panel;

    /// <summary>If <see cref="panel"/> is set, flips its active state (visible ↔ hidden).</summary>
    public void TogglePanel()
    {
        if (panel != null)
        {
            // Switches between active and inactive
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);
        }
    }
}
