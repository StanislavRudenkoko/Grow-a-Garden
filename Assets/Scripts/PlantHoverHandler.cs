/// <summary>
/// PlantHoverHandler
/// Author: Stanislav Rudenko, Joshua Trepanier
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// Bridges pointer hover on a pot to <see cref="TooltipManager"/> (instance tooltip).
/// </summary>
public class PlantHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PlantInstance plantInstance;
    public PlantInstance GetInstance() => plantInstance;

    /// <summary>Caches the <see cref="PlantInstance"/> this handler belongs to.</summary>
    public void Initialize(PlantInstance instance)
    {
        plantInstance = instance;
    }

    /// <summary>
    /// Shows tooltips when cursor is hovering over a pot with a plant growing
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TooltipManager.Instance == null || plantInstance == null)
            return;

        if (!plantInstance.hasPlant)
            return;

        // Use new Input System to get mouse position
        Vector2 mousePos = Mouse.current.position.ReadValue();
        mousePos.y -= 50f;
        TooltipManager.Instance.RequestShow(plantInstance, mousePos);
    }

    /// <summary>
    /// Hides all tooltips when the cursor leaves a specific space
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (TooltipManager.Instance == null)
            return;

        TooltipManager.Instance.HideAll();
    }
}
