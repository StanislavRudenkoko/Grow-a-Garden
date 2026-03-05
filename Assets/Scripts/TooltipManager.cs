using UnityEngine;
using System.Collections;

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

        instanceTooltip.SetData(pendingInstance);
        instanceTooltip.Show(pendingMousePos);
    }

    public void HideAll()
    {
        if (showCoroutine != null)
            StopCoroutine(showCoroutine);

        instanceTooltip.Hide();
        definitionTooltip.Hide();
    }

    public void ShowDefinition(PlantDefinition definition, Vector2 anchorPosition)
    {
        definitionTooltip.SetData(definition);
        definitionTooltip.ShowBeside(anchorPosition);
    }
}