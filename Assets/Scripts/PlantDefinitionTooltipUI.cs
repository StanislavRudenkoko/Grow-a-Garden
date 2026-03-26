/// <summary>
/// PlantDefinitionTooltipUI
/// Author: Stanislav Rudenko
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>

using UnityEngine;
using TMPro;

/// <summary>
/// Tooltip UI showing static plant definition fields (names, family, short description).
/// </summary>
public class PlantDefinitionTooltipUI : TooltipBase
{
    public TMP_Text displayNameText;
    public TMP_Text scientificNameText;
    public TMP_Text familyText;
    public TMP_Text descriptionText;

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>Fills labels from a <see cref="PlantDefinition"/>.</summary>
    public void SetData(PlantDefinition def)
    {
        displayNameText.text = def.displayName;
        scientificNameText.text = def.scientificName;
        familyText.text = def.family;
        descriptionText.text = def.descriptionShort;
    }

    /// <summary>Shows the panel offset to the right of <paramref name="anchorPosition"/>.</summary>
    public void ShowBeside(Vector2 anchorPosition)
    {
        rectTransform.position = anchorPosition + new Vector2(350, 0);
        ClampToScreen();
        FadeIn();
    }
}
