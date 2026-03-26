/// <summary>
/// PlantHoverHandler
/// Author: Stanislav Rudenko
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TooltipManager.Instance == null || plantInstance == null)
            return;

        // Use new Input System to get mouse position
        Vector2 mousePos = Mouse.current.position.ReadValue();

        TooltipManager.Instance.RequestShow(plantInstance, mousePos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (TooltipManager.Instance == null)
            return;

        TooltipManager.Instance.HideAll();
    }
}
