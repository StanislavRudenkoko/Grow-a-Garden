/// <summary>
/// TooltipManager
/// Author: Stanislav Rudenko
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>

using UnityEngine;
using System.Collections;

/// <summary>
/// Coordinates instance and definition tooltips: delayed show on hover, hide-all, and “show more” definition panel.
/// </summary>
public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    public PlantInstanceTooltipUI instanceTooltip;
    public PlantDefinitionTooltipUI definitionTooltip;

    public float hoverDelay = 0.2f;

    private Coroutine showCoroutine;
    private PlantInstance pendingInstance;
    private Vector2 pendingMousePos;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>Schedules the instance tooltip after <see cref="hoverDelay"/>.</summary>
    public void RequestShow(PlantInstance instance, Vector2 mousePos)
    {
        pendingInstance = instance;
        pendingMousePos = mousePos;

        if (showCoroutine != null)
            StopCoroutine(showCoroutine);

        showCoroutine = StartCoroutine(DelayedShow());
    }

    private IEnumerator DelayedShow()
    {
        yield return new WaitForSeconds(hoverDelay);

        Debug.Log("Showing tooltip at mouse pos: " + pendingMousePos);

        instanceTooltip.SetData(pendingInstance);
        instanceTooltip.Show(pendingMousePos);
    }

    /// <summary>Hides both tooltips and cancels a pending delayed show.</summary>
    public void HideAll()
    {
        if (showCoroutine != null)
            StopCoroutine(showCoroutine);

        instanceTooltip.Hide();
        definitionTooltip.Hide();
    }

    /// <summary>Shows the definition tooltip beside an anchor (e.g. instance tooltip position).</summary>
    public void ShowDefinition(PlantDefinition definition, Vector2 anchorPosition)
    {
        definitionTooltip.SetData(definition);
        definitionTooltip.ShowBeside(anchorPosition);
    }
}
