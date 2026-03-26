using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlantHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PlantInstance plantInstance;
    public PlantInstance GetInstance() => plantInstance;

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