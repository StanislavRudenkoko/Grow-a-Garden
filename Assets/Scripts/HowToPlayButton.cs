using UnityEngine;

public class HowToPlayButton : MonoBehaviour
{
    public GameObject panel;

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
